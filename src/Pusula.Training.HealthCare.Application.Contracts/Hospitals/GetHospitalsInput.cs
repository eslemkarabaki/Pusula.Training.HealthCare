using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Hospitals
{
    public class GetHospitalsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; } 

        public GetHospitalsInput()
        {
        }
    }
}
