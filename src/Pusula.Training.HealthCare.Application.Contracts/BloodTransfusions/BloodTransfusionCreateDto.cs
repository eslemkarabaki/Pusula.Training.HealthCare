using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.BloodTransfusions;

public class BloodTransfusionCreateDto
{
    [Required]
    [StringLength(BloodTransfusionConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}