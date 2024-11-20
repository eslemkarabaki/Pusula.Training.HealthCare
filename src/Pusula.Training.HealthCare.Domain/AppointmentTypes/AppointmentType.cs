using JetBrains.Annotations;
using Pusula.Training.HealthCare.Departments;
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
            Check.NotNull(name, nameof(name), AppointmentTypeConsts.NameMaxLength);

            Id=id; 
            Name=name;
        }
    }
}
