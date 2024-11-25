using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class RadiologyExamination : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string ExaminationCode { get; set; }

        public Guid GroupId { get; set; }

        protected RadiologyExamination()
        {
            Name = string.Empty;
            ExaminationCode = string.Empty;
            GroupId = Guid.Empty;
        }

        public RadiologyExamination(Guid id, string name, string examinationCode, Guid groupId)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), RadiologyExaminationConsts.NameMaxLength, 0);
            Name = name;
            Check.NotNull(examinationCode, nameof(examinationCode));
            Check.Length(examinationCode, nameof(examinationCode), RadiologyExaminationConsts.MaxCodeLength, 0);
            ExaminationCode = examinationCode;
            GroupId = groupId;
        }
    }
}
