    using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Examinations
{
    public class Examination : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string IdentityNumber { get; set; }
        public DateTime VisitDate { get; set; }
        public virtual string? Notes { get; set; }
        public string ChronicDiseases { get; set; }
        public string Allergies { get; set; }
        public string Medications { get; set; }
        public string Diagnosis { get; set; } 
        public string Prescription  { get; set; }
        public string ImagingResults { get; set; }
  
        public Guid PatientId { get; set; }    
        public Guid DoctorId { get; set; }

        protected Examination()
        {
            IdentityNumber = string.Empty;
            Notes = string.Empty;
            ChronicDiseases = string.Empty;
            Allergies = string.Empty;
            Medications = string.Empty;
            Diagnosis = string.Empty;
            Prescription = string.Empty;
            ImagingResults = string.Empty;
        }

        public Examination(Guid patientId, Guid doctorId, string? identityNumber, string? notes, DateTime visitDate, string? chronicDiseases, string? allergies, string? medications, string? diagnosis, string? prescription, string? imagingResults)
        {

            Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
            Check.NotDefaultOrNull<Guid>(doctorId, nameof(doctorId));
            Check.NotNull(identityNumber, nameof(identityNumber));
            Check.Length(identityNumber, nameof(identityNumber), ExaminationConsts.IdentityNumberMaxLength, 0);
            Check.NotNull(notes, nameof(notes));    
            Check.Length(notes, nameof(notes), ExaminationConsts.NotesNumberMaxLength, 0);
            Check.NotNull(chronicDiseases, nameof(chronicDiseases));
            Check.Length(chronicDiseases, nameof(chronicDiseases), ExaminationConsts.ChronicDiseasesNumberMaxLength, 0);
            Check.NotNull(allergies, nameof(allergies));
            Check.Length(allergies, nameof(allergies), ExaminationConsts.AllergiesNumberMaxLength, 0);
            Check.NotNull(medications, nameof(medications));
            Check.Length(medications, nameof(medications), ExaminationConsts.MedicationsNumberMaxLength, 0);
            Check.NotNull(diagnosis, nameof(diagnosis));
            Check.Length(diagnosis, nameof(diagnosis), ExaminationConsts.DiagnosisNumberMaxLength, 0);
            Check.NotNull(prescription, nameof(prescription));
            Check.Length(prescription, nameof(prescription), ExaminationConsts.PrescriptionNumberMaxLength, 0);
            Check.NotNull(imagingResults, nameof(imagingResults));
            Check.Length(imagingResults, nameof(imagingResults), ExaminationConsts.ImagingResultsNumberMaxLength, 0);
            


            PatientId = patientId;
            DoctorId = doctorId;
            Notes = notes;
            ChronicDiseases = chronicDiseases;
            VisitDate = visitDate;
            IdentityNumber = identityNumber;
            Allergies = allergies;
            Medications = medications;
            Diagnosis = diagnosis;
            Prescription = prescription;
            ImagingResults = imagingResults;
        }

    }
}
