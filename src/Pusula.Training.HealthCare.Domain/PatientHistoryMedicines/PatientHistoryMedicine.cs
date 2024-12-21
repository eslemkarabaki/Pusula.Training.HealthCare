using System;
using Pusula.Training.HealthCare.Medicines;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryMedicines;

public class PatientHistoryMedicine : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid MedicineId { get; set; }
    public string Explanation { get; set; }
    public bool NotExist { get; set; }

    protected PatientHistoryMedicine() => Explanation = string.Empty;

    public PatientHistoryMedicine(Guid patientHistoryId, Guid medicineId, string explanation, bool notExist)
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        MedicineId = Check.NotDefaultOrNull<Guid>(medicineId, nameof(medicineId));
        Explanation = Check.NotNullOrWhiteSpace(explanation, nameof(explanation), MedicineConsts.ExplanationMaxLength);
        NotExist = notExist;
    }

    public override object?[] GetKeys() => [PatientHistoryId, MedicineId];
}