using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public class RadiologyExaminationProcedureDto : FullAuditedEntityDto<Guid>
    {
        public string Result { get; set; } = null!;
        public DateTime ResultDate { get; set; } = DateTime.Now;
        public Guid DoctorId { get; set; } = Guid.Empty;
        public Guid ProtocolId { get; set; } = Guid.Empty;
        public Guid RadiologyExaminationId { get; set; } = Guid.Empty;
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
