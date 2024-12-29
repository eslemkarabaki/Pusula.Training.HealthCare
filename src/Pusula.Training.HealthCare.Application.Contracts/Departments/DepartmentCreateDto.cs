using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Departments;

public class DepartmentCreateDto
{
    [Required]
    [StringLength(DepartmentConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [StringLength(DepartmentConsts.DescriptionMaxLength)]
    public string? Description { get; set; }

    [Required]
    [Range(1, DepartmentConsts.DurationMaxValue)]
    public int Duration { get; set; }
     
}