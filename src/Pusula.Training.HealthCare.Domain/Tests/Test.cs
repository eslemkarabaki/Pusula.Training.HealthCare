using System;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.TestTypes;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Tests
{
    public class Test : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        public virtual Guid GroupId { get; set; }
        public virtual TestGroup TestGroup { get; set; }

        public virtual Guid TestTypeId { get; set; } // Tetkik Tipi ID'si
        public virtual TestType TestType { get; set; } // Tetkik Tipi ile ilişki

        protected Test()
        {
            Code = string.Empty;
            Name = string.Empty;
        }

        public Test(Guid id, string code, string name, Guid groupId, Guid testTypeId)
        {
            Id = id;
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Code = code;
            Name = name;
            GroupId = groupId;
            TestTypeId = testTypeId;
        }
    }
}
