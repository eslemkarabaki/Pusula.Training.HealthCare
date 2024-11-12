using JetBrains.Annotations;  
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Notifications
{
    public class NotificationManager(INotificationRepository notificationRepository) : DomainService
    {
        public virtual async Task<Notification> CreateAsync(
            string notificationType, 
            string notificationMessage,
            bool notificationStatus,
            DateTime notificationDate)
        {
            Check.NotNullOrWhiteSpace(notificationType, nameof(notificationType));
            Check.Length(notificationMessage, nameof(notificationMessage), NotificationConsts.NotificationMessageMaxLength, 0);
            Check.NotNull(notificationStatus, nameof(notificationStatus));
            Check.NotNull(notificationDate, nameof(notificationDate));


            var notification = new Notification(
             GuidGenerator.Create(),
             notificationType,
             notificationMessage,
             notificationStatus,
             notificationDate 
            );

            return await notificationRepository.InsertAsync(notification);
        }

        public virtual async Task<Notification> UpdateAsync(
            Guid id,
            string notificationType,
            string notificationMessage,
            bool notificationStatus,
            DateTime notificationDate,
            [CanBeNull] string? concurrencyStamp = null)
        {
            
            Check.NotNullOrWhiteSpace(notificationType, nameof(notificationType));
            Check.Length(notificationMessage, nameof(notificationMessage), NotificationConsts.NotificationMessageMaxLength, 0);
            Check.NotNull(notificationStatus, nameof(notificationStatus));
            Check.NotNull(notificationDate, nameof(notificationDate));

            var hospital = await notificationRepository.GetAsync(id);

            hospital.NotificationType = notificationType;
            hospital.NotificationMessage = notificationMessage;
            hospital.NotificationStatus = notificationStatus;
            hospital.NotificationDate = notificationDate;

            hospital.SetConcurrencyStampIfNotNull(concurrencyStamp);

            return await notificationRepository.UpdateAsync(hospital);
        }
    }
}
