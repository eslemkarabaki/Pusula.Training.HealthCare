using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Notifications
{
    public class Notification: AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string NotificationType { get; set; }
        
        [NotNull]
        public virtual string NotificationMessage { get; set; }

        [NotNull]
        public virtual bool NotificationStatus { get; set; }  

        [NotNull]
        public virtual DateTime NotificationDate { get; set; }

        protected Notification()
        {
            NotificationType = string.Empty;
            NotificationMessage = string.Empty;
            NotificationStatus = false;
            NotificationDate = DateTime.Now;
        }

        public Notification(Guid id, string notificationType, string notificationMessage, bool notificationStatus, DateTime notificationDate)
        {
            Id = id;
            Check.NotNull(notificationType, nameof(notificationType)); 
            NotificationType = notificationType;
            Check.Length(notificationMessage, nameof(notificationMessage), NotificationConsts.NotificationMessageMaxLength, 0);
            NotificationMessage = notificationMessage;
            Check.NotNull(notificationStatus, nameof(notificationStatus));
            NotificationStatus = notificationStatus;
            Check.NotNull(notificationDate, nameof(notificationDate));
            NotificationDate = notificationDate;
        }
    }
}
