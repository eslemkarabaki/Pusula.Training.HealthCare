using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Vaccines;

public class VaccineCreateDto
{
    [Required]
    [StringLength(VaccineConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}