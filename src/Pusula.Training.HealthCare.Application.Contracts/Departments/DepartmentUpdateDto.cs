using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Departments;

public class DepartmentUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(DepartmentConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [StringLength(DepartmentConsts.DescriptionMaxLength)]
    public string? Description { get; set; }

    [Required]
    [Range(1, DepartmentConsts.DurationMaxValue)]
    public int Duration { get; set; }
    public string[]? HospitalNames { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;
}