
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Insurances
{
    public class GetInsurancesInput: PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public string? Name { get; set; }
        public GetInsurancesInput() { }
    }
}
