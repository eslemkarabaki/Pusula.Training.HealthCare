using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Examinations;

public class ExaminationCreateDto
{
    [Required]
    [StringLength(ExaminationConsts.IdentityNumberMaxLength)]
    public string IdentityNumber { get; set; } = null!;

    [Required]
    [StringLength(ExaminationConsts.NotesNumberMaxLength)]
    public string Notes { get; set; } = null!;

    [Required] public DateTime VisitDate { get; set; } = DateTime.Now;

    [Required]
    [StringLength(ExaminationConsts.ChronicDiseasesNumberMaxLength)]
    public string ChronicDiseases { get; set; } = null!;

    [Required]
    [StringLength(ExaminationConsts.AllergiesNumberMaxLength)]
    public string Allergies { get; set; } = null!;

    [Required]
    [StringLength(ExaminationConsts.MedicationsNumberMaxLength)]
    public string Medications { get; set; } = null!;

    [Required]
    [StringLength(ExaminationConsts.DiagnosisNumberMaxLength)]
    public string Diagnosis { get; set; }
    [Required]
    [StringLength(ExaminationConsts.PrescriptionNumberMaxLength)]
    public string Prescription { get; set; }
    [Required]
    [StringLength(ExaminationConsts.ImagingResultsNumberMaxLength)]
    public string? ImagingResults { get; set; }
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
}