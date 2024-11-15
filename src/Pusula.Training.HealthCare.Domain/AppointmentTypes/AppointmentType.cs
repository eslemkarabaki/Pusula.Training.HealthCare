using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentType : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        protected AppointmentType() {
            Name = string.Empty;
        }

        public AppointmentType(Guid id, string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Id = id;
            Name = name;
        }
    }
}
