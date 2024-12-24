using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.BloodTransfusions;
using Pusula.Training.HealthCare.DataAnnotations;

namespace Pusula.Training.HealthCare.PatientHistoryBloodTransfusions;

public class PatientHistoryBloodTransfusionCreateDto
{
    [Required]
    [NotEmptyGuid]
    public Guid PatientHistoryId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? BloodTransfusionId { get; set; }

    [StringLength(BloodTransfusionConsts.ExplanationMaxLength)]
    public string? Explanation { get; set; }
}