using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public class GetRadiologyExaminationProceduresInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Result { get; set; }
        public DateTime? ResultDate { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? ProtocolId { get; set; }
        public Guid? RadiologyExaminationId { get; set; }
        public GetRadiologyExaminationProceduresInput()
        {
        }
    }
}
