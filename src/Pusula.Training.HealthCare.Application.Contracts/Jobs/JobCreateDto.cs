using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Jobs;

public class JobCreateDto
{
    [Required]
    [StringLength(JobConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}