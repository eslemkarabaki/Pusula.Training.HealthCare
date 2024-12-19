using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Districts;

public class EfCoreDistrictRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, District, Guid>(dbContextProvider), IDistrictRepository
{
    public async Task<List<District>> GetListAsync(
        string? filterText = null,
        string? name = null,
        Guid? cityId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), filterText, name, cityId)
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<List<District>> GetListWithDetailsAsync(
        string? filterText = null,
        string? name = null,
        Guid? cityId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await WithDetailsAsync(e => e.City.Country), filterText, name, cityId)
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<long> GetCountAsync(
        string? filterText = null,
        string? name = null,
        Guid? cityId = null,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), filterText, name, cityId)
            .LongCountAsync(GetCancellationToken(cancellationToken));

    public async Task DeleteAllAsync(
        string? filterText = null,
        string? name = null,
        Guid? cityId = null,
        CancellationToken cancellationToken = default
    )
    {
        var ids = ApplyFilter(await GetQueryableAsync(), filterText, name, cityId)
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<District> ApplyFilter(
        IQueryable<District> query,
        string? filterText = null,
        string? name = null,
        Guid? cityId = null
    ) =>
        query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => EF.Functions.ILike(e.Name, filterText!))
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => EF.Functions.ILike(e.Name, name!))
            .WhereIf(cityId.HasValue, e => e.CityId == cityId!.Value);

    protected virtual string GetSorting(string? sorting, bool withEntityName) =>
        sorting.IsNullOrWhiteSpace() ?
            DistrictConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "District." : string.Empty)}{sorting}";
}