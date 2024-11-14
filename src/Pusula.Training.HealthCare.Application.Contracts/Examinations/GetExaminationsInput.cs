using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Examinations
{
    public class GetExaminationsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public virtual string? IdentityNumber { get; set; }
        public virtual DateTime? VisitDate { get; set; }
        public virtual string? Notes { get; set; }
        public string? ChronicDiseases { get; set; }
        public string? Allergies { get; set; }
        public string? Medications { get; set; }
        public string? Diagnosis { get; set; }
        public string? Prescription { get; set; }
        public string? ImagingResults { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
    }
}
