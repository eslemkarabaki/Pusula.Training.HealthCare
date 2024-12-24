using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;
using Pusula.Training.HealthCare.Medicines;

namespace Pusula.Training.HealthCare.PatientHistoryMedicines;

public class PatientHistoryMedicineCreateDto
{
    [Required]
    [NotEmptyGuid]
    public Guid PatientHistoryId { get; set; }

    [Required]
    [NotEmptyGuid]
    public Guid? MedicineId { get; set; }

    [StringLength(MedicineConsts.ExplanationMaxLength)]
    public string? Explanation { get; set; }
}