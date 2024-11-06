using System;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Departments;

public class DepartmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Duration { get; set; }
    public string[]? HospitalNames { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;

}