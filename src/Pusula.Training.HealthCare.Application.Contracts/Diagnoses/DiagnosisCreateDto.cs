using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Diagnoses;

public class DiagnosisCreateDto
{
    [Required]
    [StringLength(DiagnosisConsts.CodeMaxLength)]
    public string Code { get; set; }

    [Required]
    [StringLength(DiagnosisConsts.NameMaxLength)]
    public string Name { get; set; }
}
