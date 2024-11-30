using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Districts;

public class EfCoreDistrictRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, District, Guid>(dbContextProvider), IDistrictRepository
{
    public async Task<List<DistrictWithCity>> GetListAsync(
        string? filterText = null,
        string? name = null,
        Guid? cityId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    )
    {
        return await ApplyFilter(await GetQueryForDistrictWithCityAsync(), filterText, name, cityId)
                     .OrderBy(e => sorting.IsNullOrWhiteSpace() ? DistrictConsts.GetDefaultSorting(false) : sorting)
                     .PageBy(skipCount, maxResultCount)
                     .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<long> GetCountAsync(string? filterText = null, string? name = null, Guid? cityId = null,
                                          CancellationToken cancellationToken = default)
    {
        return await ApplyFilter(await GetQueryableAsync(), filterText, name, cityId)
            .LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task DeleteAllAsync(string? filterText = null, string? name = null, Guid? cityId = null,
                                     CancellationToken cancellationToken = default)
    {
        var ids = ApplyFilter(await GetQueryableAsync(), filterText, name, cityId)
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<DistrictWithCity>> GetQueryForDistrictWithCityAsync()
    {
        return from district in await GetDbSetAsync()
               join city in (await GetDbContextAsync()).Set<City>() on district.CityId equals
                   city.Id into
                   cities
               from city in cities.DefaultIfEmpty()
               select new DistrictWithCity
               {
                   Id = district.Id,
                   Name = district.Name,
                   CityId = city.Id,
                   City = city.Name
               };
    }

    protected virtual IQueryable<District> ApplyFilter(
        IQueryable<District> query,
        string? filterText = null,
        string? name = null,
        Guid? cityId = null)
    {
        return query
               .Where(e => !e.IsDeleted)
               .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
               .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
               .WhereIf(cityId.HasValue, e => e.CityId == cityId!.Value);
    }

    protected virtual IQueryable<DistrictWithCity> ApplyFilter(
        IQueryable<DistrictWithCity> query,
        string? filterText = null,
        string? name = null,
        Guid? cityId = null
    )
    {
        return query
               .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
               .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
               .WhereIf(cityId.HasValue, e => e.CityId == cityId!.Value);
    }
}