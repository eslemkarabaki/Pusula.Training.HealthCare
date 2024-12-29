using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public class RadiologyExaminationProcedureUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(RadiologyExaminationProcedureConsts.MaxResultLength)]
        public string Result { get; set; } = null!;
        [Required]
        public DateTime ResultDate { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ProtocolId { get; set; }
        public Guid RadiologyExaminationId { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
