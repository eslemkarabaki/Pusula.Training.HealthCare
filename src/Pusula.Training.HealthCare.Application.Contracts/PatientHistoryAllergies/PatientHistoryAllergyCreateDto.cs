using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.Allergies;
using Pusula.Training.HealthCare.DataAnnotations;

namespace Pusula.Training.HealthCare.PatientHistoryAllergies;

public class PatientHistoryAllergyCreateDto
{
    [Required]
    [NotEmptyGuid]
    public Guid PatientHistoryId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? AllergyId { get; set; }

    [StringLength(AllergyConsts.ExplanationMaxLength)]
    public string? Explanation { get; set; }
}