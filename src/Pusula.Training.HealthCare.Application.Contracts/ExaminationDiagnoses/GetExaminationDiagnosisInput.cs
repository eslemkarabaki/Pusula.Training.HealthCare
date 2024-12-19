using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class GetExaminationDiagnosisInput : PagedAndSortedResultRequestDto
    {
        public Guid? ExaminationId { get; set; }
        public Guid? DiagnosisId { get; set; }
        public string? Explanation { get; set; }
        public string? Type { get; set; }
    }
}
