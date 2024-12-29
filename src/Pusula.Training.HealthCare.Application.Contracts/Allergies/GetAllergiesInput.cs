using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Allergies;

public class GetAllergiesInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
}