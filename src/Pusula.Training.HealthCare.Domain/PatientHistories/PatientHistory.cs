using System;
using System.Collections;
using System.Collections.Generic;
using Pusula.Training.HealthCare.Educations;
using Pusula.Training.HealthCare.Jobs;
using Pusula.Training.HealthCare.PatientHistoryAllergies;
using Pusula.Training.HealthCare.PatientHistoryBloodTransfusions;
using Pusula.Training.HealthCare.PatientHistoryMedicines;
using Pusula.Training.HealthCare.PatientHistoryOperations;
using Pusula.Training.HealthCare.PatientHistoryVaccines;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PatientHistories;

public class PatientHistory : FullAuditedAggregateRoot<Guid>
{
    public Guid PatientId { get; private set; }

    public Guid JobId { get; private set; }
    public Job Job { get; set; }

    public Guid EducationId { get; private set; }
    public Education Education { get; set; }

    public ICollection<EnumPatientHabit> Habits { get; private set; }
    public ICollection<EnumSpecialCase> SpecialCases { get; private set; }
    public ICollection<EnumBodyDevice> BodyDevices { get; private set; }
    public ICollection<EnumTherapy> Therapies { get; private set; }

    public ICollection<PatientHistoryAllergy> Allergies { get; set; }
    public ICollection<PatientHistoryBloodTransfusion> BloodTransfusions { get; set; }
    public ICollection<PatientHistoryMedicine> Medicines { get; set; }
    public ICollection<PatientHistoryVaccine> Vaccines { get; set; }
    public ICollection<PatientHistoryOperation> Operations { get; set; }

    protected PatientHistory()
    {
        Habits = [];
        SpecialCases = [];
        BodyDevices = [];
        Therapies = [];
        Allergies = [];
        BloodTransfusions = [];
        Medicines = [];
        Vaccines = [];
        Operations = [];
    }

    public PatientHistory(
        Guid id,
        Guid patientId,
        Guid jobId,
        Guid educationId
    ) : base(id)
    {
        SetPatientId(patientId);
        SetJobId(jobId);
        SetEducationId(educationId);
        Habits = [];
        SpecialCases = [];
        BodyDevices = [];
        Therapies = [];
        Allergies = [];
        BloodTransfusions = [];
        Medicines = [];
        Vaccines = [];
        Operations = [];
    }

    public void SetPatientId(Guid patientId) => PatientId = Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
    public void SetJobId(Guid jobId) => JobId = Check.NotDefaultOrNull<Guid>(jobId, nameof(jobId));

    public void SetEducationId(Guid educationId) =>
        EducationId = Check.NotDefaultOrNull<Guid>(educationId, nameof(educationId));

    public void SetHabits(ICollection<EnumPatientHabit> habits) => Habits = habits;

    public void SetSpecialCases(ICollection<EnumSpecialCase> specialCases) => SpecialCases = specialCases;

    public void SetBodyDevices(ICollection<EnumBodyDevice> bodyDevices) => BodyDevices = bodyDevices;

    public void SetTherapies(ICollection<EnumTherapy> therapies) => Therapies = therapies;
}