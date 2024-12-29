using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Tests;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestGroups
{
    public class TestGroup : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Code { get; set; }

        [NotNull]
        public virtual string Name { get; set; } // Örneğin: hormon, biyokimya, genetik

        public virtual ICollection<Test> Tests { get; set; }

        protected TestGroup()
        {
            Code = string.Empty;
            Name = string.Empty;
            Tests = new List<Test>();
        }

        public TestGroup(Guid id, string code, string name)
        {
            Id = id;
            Check.NotNullOrWhiteSpace(code, nameof(code));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Code = code;
            Name = name;
            Tests = new List<Test>();
        }
    }
}
