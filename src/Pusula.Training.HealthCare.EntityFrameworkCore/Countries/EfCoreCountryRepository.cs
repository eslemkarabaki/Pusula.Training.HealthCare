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

namespace Pusula.Training.HealthCare.Countries;

public class EfCoreCountryRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Country, Guid>(dbContextProvider), ICountryRepository
{
    public async Task<List<Country>> GetListAsync(
        string? filterText = null,
        string? name = null,
        string? iso = null,
        string? phoneCode = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), filterText, name, iso, phoneCode)
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<long> GetCountAsync(
        string? filterText = null,
        string? name = null,
        string? iso = null,
        string? phoneCode = null,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), filterText, name, iso, phoneCode)
            .LongCountAsync(GetCancellationToken(cancellationToken));

    public async Task DeleteAllAsync(
        string? filterText = null,
        string? name = null,
        string? iso = null,
        string? phoneCode = null,
        CancellationToken cancellationToken = default
    )
    {
        var ids = ApplyFilter(await GetQueryableAsync(), filterText, name, iso, phoneCode)
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<Country> ApplyFilter(
        IQueryable<Country> query,
        string? filterText = null,
        string? name = null,
        string? iso = null,
        string? phoneCode = null
    ) =>
        query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
            .WhereIf(!string.IsNullOrWhiteSpace(iso), e => e.Iso.Contains(iso!))
            .WhereIf(!string.IsNullOrWhiteSpace(phoneCode), e => e.PhoneCode.Contains(phoneCode!));

    protected virtual string GetSorting(string? sorting, bool withEntityName) =>
        sorting.IsNullOrWhiteSpace() ?
            CountryConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "Country." : string.Empty)}{sorting}";
}