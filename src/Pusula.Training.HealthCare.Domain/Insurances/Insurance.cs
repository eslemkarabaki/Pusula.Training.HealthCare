using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Insurances
{
    public class Insurance:FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; private set; }

        protected Insurance() 
        {
            Name = string.Empty;
        }

        public Insurance(Guid id, string name)
        {
            SetName(name);
        }
        public void SetName(string name) => Name = Check.NotNull(name, nameof(name), InsuranceConsts.NameMaxLength);
    }

}
