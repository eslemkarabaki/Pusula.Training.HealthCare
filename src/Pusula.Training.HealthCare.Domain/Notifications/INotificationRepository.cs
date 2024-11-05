using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Notifications
{
    public interface INotificationRepository : IRepository<Notification, Guid>
    {
        Task DeleteAllAsync(
        string? filterText = null,
        string? notificationType = null,
        string? notificationMessage = null,
        bool? notificationStatus = null,
        DateTime? notificationDate = null,
        CancellationToken cancellationToken = default);
        Task<List<Notification>> GetListAsync(
                    string? filterText = null, 
                    string? notificationType = null,
                    string? notificationMessage = null,
                    bool? notificationStatus = null,
                    DateTime? notificationDate = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null, 
            string? notificationType = null,
            string? notificationMessage = null,
            bool? notificationStatus = null,
            DateTime? notificationDate = null,
            CancellationToken cancellationToken = default);
    }
}
