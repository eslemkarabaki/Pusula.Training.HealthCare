using Volo.Abp.Application.Dtos;
using System;

namespace Pusula.Training.HealthCare.TestGroups;

public class GetTestGroupsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
}
