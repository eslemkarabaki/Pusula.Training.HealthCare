using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;
using Pusula.Training.HealthCare.Vaccines;

namespace Pusula.Training.HealthCare.PatientHistoryVaccines;

public class PatientHistoryVaccineCreateDto
{
    [Required]
    [NotEmptyGuid]
    public Guid PatientHistoryId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? VaccineId { get; set; }

    [StringLength(VaccineConsts.ExplanationMaxLength)]
    public string? Explanation { get; set; }
}