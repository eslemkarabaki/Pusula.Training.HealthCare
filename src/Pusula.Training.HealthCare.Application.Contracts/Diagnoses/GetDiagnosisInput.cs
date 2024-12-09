using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class GetDiagnosisInput : PagedAndSortedResultRequestDto
    {
        public string? Name { get; set; }
    }
}
