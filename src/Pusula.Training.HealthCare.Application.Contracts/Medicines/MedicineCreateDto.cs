using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Medicines;

public class MedicineCreateDto
{
    [Required]
    [StringLength(MedicineConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
}