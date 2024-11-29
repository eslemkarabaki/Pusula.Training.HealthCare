using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class RadiologyExaminationManager(IRadiologyExaminationRepository radiologyExaminationRepository) : DomainService
    {
        public virtual async Task<RadiologyExamination> CreateAsync(
            string name,
            string examinationCode,
            Guid groupId) 
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), RadiologyExaminationConsts.NameMaxLength);
            Check.NotNullOrWhiteSpace(examinationCode, nameof(examinationCode));
            Check.Length(examinationCode, nameof(examinationCode), RadiologyExaminationConsts.MaxCodeLength);

            var radiologyExamination = new RadiologyExamination(
                GuidGenerator.Create(),
                name,
                examinationCode,
                groupId
            );

            return await radiologyExaminationRepository.InsertAsync(radiologyExamination);
        }

        public virtual async Task<RadiologyExamination> UpdateAsync(
            Guid id,
            string name,
            string examinationCode,
            Guid groupId,
            [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), RadiologyExaminationConsts.NameMaxLength);
            Check.NotNullOrWhiteSpace(examinationCode, nameof(examinationCode));
            Check.Length(examinationCode, nameof(examinationCode), RadiologyExaminationConsts.MaxCodeLength);

            var radiologyExamination = await radiologyExaminationRepository.FindAsync(id);

            radiologyExamination!.Name = name;
            radiologyExamination.ExaminationCode = examinationCode;
            radiologyExamination.GroupId = groupId;

            return await radiologyExaminationRepository.UpdateAsync(radiologyExamination);
        }
    } 
}
