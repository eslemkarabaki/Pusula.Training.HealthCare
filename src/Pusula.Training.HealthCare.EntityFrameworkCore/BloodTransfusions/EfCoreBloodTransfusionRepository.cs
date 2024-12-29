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

namespace Pusula.Training.HealthCare.BloodTransfusions;

public class EfCoreBloodTransfusionRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, BloodTransfusion, Guid>(dbContextProvider), IBloodTransfusionRepository
{
    public async Task<List<BloodTransfusion>> GetListAsync(
        string? name = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), name)
              .OrderBy(
                  string.IsNullOrEmpty(sorting) ?
                      BloodTransfusionConsts.GetDefaultSorting(false) :
                      sorting
              )
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task DeleteAllAsync(string? name = null, CancellationToken cancellationToken = default)
    {
        var ids = ApplyFilter(await GetQueryableAsync(), name)
            .Select(e => e.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<BloodTransfusion> ApplyFilter(
        IQueryable<BloodTransfusion> query,
        string? name = null
    ) =>
        query
            .WhereIf(!string.IsNullOrWhiteSpace(name), e => EF.Functions.ILike(e.Name, $"{name!}%"));
}