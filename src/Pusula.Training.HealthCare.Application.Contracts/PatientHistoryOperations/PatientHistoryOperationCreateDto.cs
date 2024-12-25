using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;
using Pusula.Training.HealthCare.Operations;

namespace Pusula.Training.HealthCare.PatientHistoryOperations;

public class PatientHistoryOperationCreateDto
{
    [Required]
    [NotEmptyGuid]
    public Guid PatientHistoryId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? OperationId { get; set; }

    [StringLength(OperationConsts.ExplanationMaxLength)]
    public string? Explanation { get; set; }
}