using System;
using Pusula.Training.HealthCare.Medicines;
using Pusula.Training.HealthCare.PatientHistories;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryMedicines;

public class PatientHistoryMedicine : Entity
{
    public Guid PatientHistoryId { get; set; }
    public virtual PatientHistory PatientHistory { get; set; }
    public Guid MedicineId { get; set; }
    public virtual Medicine Medicine { get; set; }
    public string? Explanation { get; set; }

    protected PatientHistoryMedicine() => Explanation = string.Empty;

    public PatientHistoryMedicine(Guid patientHistoryId, Guid medicineId, string? explanation)
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        MedicineId = Check.NotDefaultOrNull<Guid>(medicineId, nameof(medicineId));
        Explanation = Check.Length(explanation, nameof(explanation), MedicineConsts.ExplanationMaxLength);
    }

    public override object?[] GetKeys() => [PatientHistoryId, MedicineId];
}