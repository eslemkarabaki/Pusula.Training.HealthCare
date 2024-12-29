using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses;

public class ExaminationDiagnosisUpdateDto
{
    [Required]
    public Guid ExaminationId { get; set; }

    [Required]
    public Guid DiagnosisId { get; set; }

    [Required]
    [MaxLength(ExaminationDiagnosisConsts.ExplanationMaxLength)]
    public string? Explanation { get; set; } = string.Empty;

    [Required]
    [MaxLength(ExaminationDiagnosisConsts.TypeMaxLength)]
    public string? Type { get; set; } = null!;
}
