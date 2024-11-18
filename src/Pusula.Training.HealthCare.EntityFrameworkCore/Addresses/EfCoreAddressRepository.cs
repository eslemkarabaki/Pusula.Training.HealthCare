using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Patients;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Addresses;

public class EfCoreAddressRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Address, Guid>(dbContextProvider), IAddressRepository
{
    public async Task<List<AddressView>> GetViewListAsync(Guid? patientId = null, string? sorting = null,
                                                          int maxResultCount = int.MaxValue, int skipCount = 0,
                                                          CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetQueryForViewAsync(), patientId)
                     .OrderBy(string.IsNullOrWhiteSpace(sorting) ? AddressConsts.GetDefaultSorting(false) : sorting)
                     .PageBy(skipCount, maxResultCount)
                     .ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<AddressView>> GetQueryForViewAsync()
    {
        var dbContext = await GetDbContextAsync();
        return from address in dbContext.Set<Address>()
               join district in dbContext.Set<District>()
                   on address.DistrictId equals district.Id into districts
               from district in districts.DefaultIfEmpty()
               join city in dbContext.Set<City>()
                   on district.CityId equals city.Id into cities
               from city in cities.DefaultIfEmpty()
               join country in dbContext.Set<Country>()
                   on city.CountryId equals country.Id into countries
               from country in countries.DefaultIfEmpty()
               select new AddressView()
               {
                   Id = address.Id,
                   PatientId = address.PatientId,
                   DistrictId = district.Id,
                   District = district.Name,
                   CityId = city.Id,
                   City = city.Name,
                   CountryId = country.Id,
                   Country = country.Name,
                   AddressTitle = address.AddressTitle,
                   AddressLine = address.AddressLine
               };
    }

    protected virtual IQueryable<AddressView> ApplyFilter(
        IQueryable<AddressView> query,
        Guid? patientId = null)
    {
        return query
            .WhereIf(patientId.HasValue, e => e.PatientId == patientId!.Value);
    }
}