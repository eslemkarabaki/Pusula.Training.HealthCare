using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Cities;

public class EfCoreCityRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, City, Guid>(dbContextProvider), ICityRepository
{
    public async Task<List<CityWithCountry>> GetListAsync(
        string? filterText = null,
        string? name = null,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    )
    {
        return await ApplyFilter(await GetQueryForCityWithCountryAsync(), filterText, name, countryId)
                     .OrderBy(e => sorting.IsNullOrWhiteSpace() ? CountryConsts.GetDefaultSorting(false) : sorting)
                     .PageBy(skipCount, maxResultCount)
                     .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<long> GetCountAsync(string? filterText = null, string? name = null, Guid? countryId = null,
                                          CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetQueryableAsync(), filterText, name, countryId)
            .LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task DeleteAllAsync(string? filterText = null, string? name = null, Guid? countryId = null,
                                     CancellationToken cancellationToken = default)
    {
        var ids = ApplyFilter(await GetQueryableAsync(), filterText, name, countryId)
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<CityWithCountry>> GetQueryForCityWithCountryAsync()
    {
        return from city in await GetDbSetAsync()
               join country in (await GetDbContextAsync()).Set<Country>() on city.CountryId equals
                   country.Id into
                   countries
               from country in countries.DefaultIfEmpty()
               select new CityWithCountry
               {
                   Id = city.Id,
                   Name = city.Name,
                   Country = country.Name,
                   CountryId = country.Id
               };
    }


    protected virtual IQueryable<City> ApplyFilter(
        IQueryable<City> query,
        string? filterText = null,
        string? name = null,
        Guid? countryId = null
    )
    {
        return query
               .Where(e => !e.IsDeleted)
               .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
               .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
               .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value);
    }

    protected virtual IQueryable<CityWithCountry> ApplyFilter(
        IQueryable<CityWithCountry> query,
        string? filterText = null,
        string? name = null,
        Guid? countryId = null
    )
    {
        return query
               .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
               .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
               .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value);
    }
}