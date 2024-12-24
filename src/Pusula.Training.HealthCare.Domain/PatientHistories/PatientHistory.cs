using System;
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

    public Guid? JobId { get; private set; }
    public Job? Job { get; set; }

    public Guid? EducationId { get; private set; }
    public Education? Education { get; set; }

    public IList<EnumPatientHabit> Habits { get; private set; }
    public IList<EnumSpecialCase> SpecialCases { get; private set; }
    public IList<EnumBodyDevice> BodyDevices { get; private set; }
    public IList<EnumTherapy> Therapies { get; private set; }

    public bool MedicinesNotExist { get; private set; }
    public bool OperationsNotExist { get; private set; }
    public bool VaccinesNotExist { get; private set; }
    public bool BloodTransfusionsNotExist { get; private set; }
    public bool AllergiesNotExist { get; private set; }

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
        Guid patientId
    ) : base(id)
    {
        SetPatientId(patientId);
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

    public void SetHabits(IList<EnumPatientHabit> habits) => Habits = habits;

    public void SetSpecialCases(IList<EnumSpecialCase> specialCases) => SpecialCases = specialCases;

    public void SetBodyDevices(IList<EnumBodyDevice> bodyDevices) => BodyDevices = bodyDevices;

    public void SetTherapies(IList<EnumTherapy> therapies) => Therapies = therapies;

    public void MedicineNotExist()
    {
        MedicinesNotExist = true;
        Medicines = [];
    }

    public void AllergyNotExist()
    {
        AllergiesNotExist = true;
        Allergies = [];
    }

    public void BloodTransfusionNotExist()
    {
        BloodTransfusionsNotExist = true;
        BloodTransfusions = [];
    }

    public void VaccineNotExist()
    {
        VaccinesNotExist = true;
        Vaccines = [];
    }

    public void OperationNotExist()
    {
        OperationsNotExist = true;
        Operations = [];
    }
}