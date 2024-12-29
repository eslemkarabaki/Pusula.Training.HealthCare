using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.PatientTypes;

public class PatientTypeCreateDto
{
    [Required]
    [StringLength(PatientTypeContst.NameMaxLength)]
    public string Name { get; set; } = null!;
}