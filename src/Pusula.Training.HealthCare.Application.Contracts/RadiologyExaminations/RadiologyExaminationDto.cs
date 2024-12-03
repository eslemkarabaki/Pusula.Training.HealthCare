using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class RadiologyExaminationDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;
        public string ExaminationCode { get; set; } = null!;
        public Guid GroupId { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
