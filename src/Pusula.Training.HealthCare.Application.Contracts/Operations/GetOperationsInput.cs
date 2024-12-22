using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Operations;

public class GetOperationsInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
}