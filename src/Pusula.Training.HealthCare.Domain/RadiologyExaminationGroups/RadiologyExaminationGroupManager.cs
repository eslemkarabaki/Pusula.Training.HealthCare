using JetBrains.Annotations;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public class RadiologyExaminationGroupManager(IRadiologyExaminationGroupRepository radiologyExaminationGroupRepository) : DomainService
    {
        public virtual async Task<RadiologyExaminationGroup> CreateAsync(
            string name,
            string description
           )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), RadiologyExaminationGroupConsts.NameMaxLength);
            Check.Length(description, nameof(description), RadiologyExaminationGroupConsts.DescriptionMaxLength);

            var radiologyExaminationGroup = new RadiologyExaminationGroup(
            GuidGenerator.Create(),
            name,
             description
             );

            return await radiologyExaminationGroupRepository.InsertAsync(radiologyExaminationGroup);
        }

        public virtual async Task<RadiologyExaminationGroup> UpdateAsync(
            Guid id,
            string name,
            [CanBeNull] string? description = null,
            [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), RadiologyExaminationGroupConsts.NameMaxLength);

            var RadiologyExaminationGroup = await radiologyExaminationGroupRepository.FindAsync(id);

            RadiologyExaminationGroup!.Name = name;

            RadiologyExaminationGroup.Description = description ?? RadiologyExaminationGroup.Description;
            Check.Length(RadiologyExaminationGroup.Description, nameof(description), RadiologyExaminationGroupConsts.DescriptionMaxLength);

            return await radiologyExaminationGroupRepository.UpdateAsync(RadiologyExaminationGroup);
        }
    }
}
