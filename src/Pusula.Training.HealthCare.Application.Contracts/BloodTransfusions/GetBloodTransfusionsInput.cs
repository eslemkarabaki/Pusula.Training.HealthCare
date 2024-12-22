using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.BloodTransfusions;

public class GetBloodTransfusionsInput : PagedAndSortedResultRequestDto
{
    public string? Name { get; set; }
}