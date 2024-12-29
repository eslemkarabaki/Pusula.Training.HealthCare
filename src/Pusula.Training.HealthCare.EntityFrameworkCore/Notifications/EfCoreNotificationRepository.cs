using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore; 

namespace Pusula.Training.HealthCare.Notifications
{
    public class EfCoreNotificationRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Notification, Guid>(dbContextProvider), INotificationRepository
    {
        public async Task DeleteAllAsync(string? filterText = null, string? notificationType = null, string? notificationMessage = null, bool? notificationStatus = null, DateTime? notificationDate = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, notificationType, notificationMessage, notificationStatus, notificationDate);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(string? filterText = null, string? notificationType = null, string? notificationMessage = null, bool? notificationStatus = null, DateTime? notificationDate = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, notificationType, notificationMessage, notificationStatus, notificationDate);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Notification>> GetListAsync(string? filterText = null, string? notificationType = null, string? notificationMessage = null, bool? notificationStatus = null, DateTime? notificationDate = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, notificationType, notificationMessage, notificationStatus, notificationDate);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? NotificationConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<Notification> ApplyFilter(
        IQueryable<Notification> query,
        string? filterText = null,
        string? notificationType = null,
        string? notificationMessage = null,
        bool? notificationStatus = null,
        DateTime? notificationDate = null)
        {
            return query 
                    .WhereIf(!string.IsNullOrWhiteSpace(notificationType), e => e.NotificationType!.Contains(notificationType!))
                    .WhereIf(!string.IsNullOrWhiteSpace(notificationMessage), e => e.NotificationMessage!.Contains(notificationMessage!))
                    .WhereIf(notificationStatus != null, e => e.NotificationStatus == notificationStatus)
                    .WhereIf(notificationDate != null, e => e.NotificationDate == notificationDate)
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText),
                        e => e.NotificationType!.Contains(filterText) ||
                             e.NotificationMessage!.Contains(filterText)); //burayı sallamasyon yaptın sanki bak tekrar

        }
    }
}
