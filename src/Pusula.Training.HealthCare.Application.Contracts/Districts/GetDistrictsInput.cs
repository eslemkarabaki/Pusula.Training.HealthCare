using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Districts;

public class GetDistrictsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; } = null!;
    public string? Name { get; set; } = null!;

    public Guid? CityId { get; set; }
}