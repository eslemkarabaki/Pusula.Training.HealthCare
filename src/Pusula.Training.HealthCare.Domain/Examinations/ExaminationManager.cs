using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp;
using Pusula.Training.HealthCare.Examinations;
using Volo.Abp.Data;

public class ExaminationManager(IExaminationRepository examinationRepository) : DomainService
{
    public virtual async Task<Examination> CreateAsync(Guid doctorId, Guid patientId, string notes, string chronicDiseases,
        DateTime visitDate,
        string identityNumber,
        string allergies, string medications, string diagnosis, string prescription, string? imagingResults = null)
        
    {
        Check.NotDefaultOrNull<Guid>(doctorId, nameof(doctorId));
        Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
        Check.NotNullOrWhiteSpace(notes, nameof(notes), ExaminationConsts.NotesNumberMaxLength);
        Check.NotNullOrWhiteSpace(chronicDiseases, nameof(chronicDiseases), ExaminationConsts.ChronicDiseasesNumberMaxLength);
        Check.NotNullOrWhiteSpace(identityNumber, nameof(identityNumber), ExaminationConsts.IdentityNumberMaxLength);
        Check.NotNullOrWhiteSpace(allergies, nameof(allergies), ExaminationConsts.AllergiesNumberMaxLength);
        Check.NotNullOrWhiteSpace(medications, nameof(medications), ExaminationConsts.MedicationsNumberMaxLength);
        Check.NotNullOrWhiteSpace(diagnosis, nameof(diagnosis), ExaminationConsts.MedicationsNumberMaxLength);
        Check.NotNullOrWhiteSpace(prescription, nameof(prescription), ExaminationConsts.MedicationsNumberMaxLength);

        if (!string.IsNullOrWhiteSpace(imagingResults))
        {
            Check.Length(imagingResults, nameof(imagingResults), ExaminationConsts.ImagingResultsNumberMaxLength);
        }

        var examination = new Examination(
           identityNumber:identityNumber,
           patientId:patientId,
           doctorId:doctorId,
           notes:notes,
           visitDate:visitDate,
           chronicDiseases:chronicDiseases,
           allergies:allergies,
           medications:medications,
           diagnosis:diagnosis,
           prescription:prescription,
           imagingResults: imagingResults
        );

        return await examinationRepository.InsertAsync(examination,autoSave:true);
    }

    public virtual async Task<Examination> UpdateAsync(
        Guid id,
        Guid doctorId, Guid patientId, string chronicDiseases, string allergies, string medications, string diagnosis, string prescription,
        DateTime visitDate,
        string identityNumber,
        string notes,string imagingResults,

        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotDefaultOrNull<Guid>(doctorId, nameof(doctorId));
        Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
        Check.NotNullOrWhiteSpace(notes, nameof(notes), ExaminationConsts.NotesNumberMaxLength);
        Check.NotNullOrWhiteSpace(chronicDiseases, nameof(chronicDiseases), ExaminationConsts.ChronicDiseasesNumberMaxLength);
        Check.NotNullOrWhiteSpace(identityNumber, nameof(identityNumber), ExaminationConsts.IdentityNumberMaxLength);
        Check.NotNullOrWhiteSpace(allergies, nameof(allergies), ExaminationConsts.AllergiesNumberMaxLength);
        Check.NotNullOrWhiteSpace(medications, nameof(medications), ExaminationConsts.MedicationsNumberMaxLength);
        Check.NotNullOrWhiteSpace(diagnosis, nameof(diagnosis), ExaminationConsts.MedicationsNumberMaxLength);
        Check.NotNullOrWhiteSpace(prescription, nameof(prescription), ExaminationConsts.MedicationsNumberMaxLength);

        if (!string.IsNullOrWhiteSpace(imagingResults))
        {
            Check.Length(imagingResults, nameof(imagingResults), ExaminationConsts.ImagingResultsNumberMaxLength);
        }

        var examination = await examinationRepository.GetAsync(id);

        examination.DoctorId = doctorId;
        examination.PatientId = patientId;
        examination.Notes = notes;
        examination.ChronicDiseases = chronicDiseases;
        examination.IdentityNumber = identityNumber;
        examination.Allergies = allergies;
        examination.Medications = medications;
        examination.Diagnosis = diagnosis;
        examination.Prescription = prescription;
        examination.ImagingResults = imagingResults;

        //examination.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await examinationRepository.UpdateAsync(examination, autoSave:true);
    }
}
