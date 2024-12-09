using System; 
using JetBrains.Annotations; 
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public class RadiologyExaminationGroup : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        protected RadiologyExaminationGroup()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public RadiologyExaminationGroup(Guid id, string name, string description)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), RadiologyExaminationGroupConsts.NameMaxLength, 0);
            Name = name;
            Check.Length(description, nameof(description), RadiologyExaminationGroupConsts.DescriptionMaxLength, 0);
            Description = description;
        }
 
    }
}
