using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Countries;

public class GetCountriesInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public string? Name { get; set; }
    public string? Iso { get; set; }
    public string? PhoneCode { get; set; }
}