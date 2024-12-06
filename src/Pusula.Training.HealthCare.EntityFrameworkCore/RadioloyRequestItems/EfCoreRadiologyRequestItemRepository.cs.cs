using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadiologyRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class EfCoreRadiologyRequestItemRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, RadiologyRequestItem, Guid>(dbContextProvider), IRadiologyRequestItemRepository
{
    #region DeleteAllAsync
    public virtual async Task DeleteAllAsync
        (
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null,
            CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, requestId, examinationId, result, resultDate, state);

        var ids = query.Select(x => x.RadiologyRequestItem.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetCountAsync
    public virtual async Task<long> GetCountAsync
        (
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null,
            CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, requestId, examinationId, result, resultDate, state);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetListAsync
    public virtual async Task<List<RadiologyRequestItem>> GetListAsync
        (
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        )
    {
        var query = ApplyFilter(await GetQueryableAsync(), filterText, requestId, examinationId, result, resultDate, state);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RadiologyRequestItemConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetListWithNavigationPropertiesAsync
    public virtual async Task<List<RadiologyRequestItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync
        (
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, requestId, examinationId, result, resultDate, state);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RadiologyRequestItemConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetWithNavigationPropertiesAsync
    public virtual async Task<RadiologyRequestItemWithNavigationProperties> GetWithNavigationPropertiesAsync
        (
            Guid id,
            CancellationToken cancellationToken = default
        )
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync()).Where(x => x.Id == id)
            .Select(x => new RadiologyRequestItemWithNavigationProperties
            {
                RadiologyRequestItem = x,
                RadiologyExamination = dbContext.Set<RadiologyExamination>().FirstOrDefault(c => c.Id == x.ExaminationId)!,
                RadiologyRequest = dbContext.Set<RadiologyRequest>().FirstOrDefault(c => c.Id == x.RequestId)!
            }).FirstOrDefault()!;
    }
    #endregion

    #region GetQueryForNavigationProperties
    protected virtual async Task<IQueryable<RadiologyRequestItemWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from radiologyRequestItem in (await GetDbSetAsync())
               join radiologyExamination in (await GetDbContextAsync()).Set<RadiologyExamination>() on radiologyRequestItem.ExaminationId equals radiologyExamination.Id into radiologyExaminations
               from radiologyExamination in radiologyExaminations.DefaultIfEmpty()
               join radiologyRequest in (await GetDbContextAsync()).Set<RadiologyRequest>() on radiologyRequestItem.RequestId equals radiologyRequest.Id into radiologyRequests
               from radiologyRequest in radiologyRequests.DefaultIfEmpty()
               select new RadiologyRequestItemWithNavigationProperties
               {
                   RadiologyExamination = radiologyExamination,
                   RadiologyRequestItem = radiologyRequestItem,
                   RadiologyRequest = radiologyRequest
               };
    }
    #endregion

    #region ApplyFilterForNavigationProperties
    protected virtual IQueryable<RadiologyRequestItemWithNavigationProperties> ApplyFilter
        (
            IQueryable<RadiologyRequestItemWithNavigationProperties> query,
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null
        )
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), x => x.RadiologyRequestItem.Result.Contains(filterText))
            .WhereIf(requestId.HasValue, x => x.RadiologyRequestItem.RequestId == requestId)
            .WhereIf(examinationId.HasValue, x => x.RadiologyRequestItem.ExaminationId == examinationId)
            .WhereIf(!string.IsNullOrWhiteSpace(result), x => x.RadiologyRequestItem.Result.Contains(result))
            .WhereIf(resultDate.HasValue, x => x.RadiologyRequestItem.ResultDate == resultDate)
            .WhereIf(state.HasValue, x => x.RadiologyRequestItem.State == state);
    }
    #endregion

    #region ApplyFilter
    protected virtual IQueryable<RadiologyRequestItem> ApplyFilter
        (
            IQueryable<RadiologyRequestItem> query,
            string? filterText = null,
            Guid? requestId = null,
            Guid? examinationId = null,
            string? result = null,
            DateTime? resultDate = null,
            RadiologyRequestItemState? state = null
        )
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), x => x.Result.Contains(filterText))
            .WhereIf(requestId.HasValue, x => x.RequestId == requestId)
            .WhereIf(examinationId.HasValue, x => x.ExaminationId == examinationId)
            .WhereIf(!string.IsNullOrWhiteSpace(result), x => x.Result.Contains(result))
            .WhereIf(resultDate.HasValue, x => x.ResultDate == resultDate)
            .WhereIf(state.HasValue, x => x.State == state);

    }
    #endregion
}