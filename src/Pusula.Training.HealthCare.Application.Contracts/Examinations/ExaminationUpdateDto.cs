using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Examinations;

public class ExaminationUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(ExaminationConsts.IdentityNumberMaxLength)]
    public string IdentityNumber { get; set; } = null!;

    [Required]
    [StringLength(ExaminationConsts.NotesNumberMaxLength)]
    public string Notes { get; set; } = null!;

    [Required] public DateTime VisitDate { get; set; }

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
    public string Diagnosis { get; set; } = null!;
    [Required]
    [StringLength(ExaminationConsts.PrescriptionNumberMaxLength)]
    public string Prescription { get; set; } = null!;
    [Required]
    [StringLength(ExaminationConsts.ImagingResultsNumberMaxLength)]
    public string ImagingResults { get; set; } = null!;
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;


}