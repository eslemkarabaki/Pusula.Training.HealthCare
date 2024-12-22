using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Educations;

public class GetEducationsInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
}