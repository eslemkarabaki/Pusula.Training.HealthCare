using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Educations;

public class EducationCreateDto
{
    [Required]
    [StringLength(EducationConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}