using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestTypes
{
    public class TestType : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        protected TestType()
        {
            Name = string.Empty;
        }

        public TestType(Guid id, string name)
        {
            Id = id;
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Name = name;
        }
    }
}
