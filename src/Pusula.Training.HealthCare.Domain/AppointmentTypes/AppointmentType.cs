using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentType : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }

        protected AppointmentType() {
            Name = string.Empty;
        }

        public AppointmentType(Guid id, string name)
        {
            SetName(name);
        }
        public void SetName(string name) => Name = Check.NotNull(name, nameof(name), AppointmentTypeConsts.NameMaxLength);

    }
}
