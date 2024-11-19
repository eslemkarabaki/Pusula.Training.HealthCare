using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.WorkLists;

public class WorkListDto : AuditedEntityDto<Guid>
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
}
