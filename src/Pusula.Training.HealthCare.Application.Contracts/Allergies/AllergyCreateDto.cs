using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Allergies;

public class AllergyCreateDto
{
    [Required]
    [StringLength(AllergyConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}