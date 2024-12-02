using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.WorkLists;

public class GetWorkListsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public Guid? DepartmentId { get; set; }
}
