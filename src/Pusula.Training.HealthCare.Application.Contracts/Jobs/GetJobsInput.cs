using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Jobs;

public class GetJobsInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
}