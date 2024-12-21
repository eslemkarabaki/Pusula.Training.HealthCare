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

namespace Pusula.Training.HealthCare.Cities;

public class EfCoreCityRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, City, Guid>(dbContextProvider), ICityRepository
{
    public async Task<List<City>> GetListAsync(
        string? filterText = null,
        string? name = null,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), filterText, name, countryId)
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<List<City>> GetListWithDetailsAsync(
        string? filterText = null,
        string? name = null,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await WithDetailsAsync(e => e.Country), filterText, name, countryId)
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<long> GetCountAsync(
        string? filterText = null,
        string? name = null,
        Guid? countryId = null,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), filterText, name, countryId)
            .LongCountAsync(GetCancellationToken(cancellationToken));

    public async Task DeleteAllAsync(
        string? filterText = null,
        string? name = null,
        Guid? countryId = null,
        CancellationToken cancellationToken = default
    )
    {
        var ids = ApplyFilter(await GetQueryableAsync(), filterText, name, countryId)
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<City> ApplyFilter(
        IQueryable<City> query,
        string? filterText = null,
        string? name = null,
        Guid? countryId = null
    ) =>
        query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => EF.Functions.ILike(e.Name, $"{filterText!}%"))
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => EF.Functions.ILike(e.Name, $"{name}%"))
            .WhereIf(countryId.HasValue, e => e.CountryId == countryId!.Value);

    protected virtual string GetSorting(string? sorting, bool withEntityName) =>
        sorting.IsNullOrWhiteSpace() ?
            CityConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "City." : string.Empty)}{sorting}";
}