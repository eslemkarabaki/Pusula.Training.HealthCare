using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Vaccines;

public class GetVaccinesInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
}