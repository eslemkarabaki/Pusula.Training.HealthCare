using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestTypes;

public class GetTestTypesInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public string? Name { get; set; }
}
