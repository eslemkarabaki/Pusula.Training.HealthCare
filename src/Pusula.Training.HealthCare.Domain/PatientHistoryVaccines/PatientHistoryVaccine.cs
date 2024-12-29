using System;
using Pusula.Training.HealthCare.Vaccines;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryVaccines;

public class PatientHistoryVaccine : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid VaccineId { get; set; }
    public virtual Vaccine Vaccine { get; set; }
    public string? Explanation { get; set; }

    protected PatientHistoryVaccine() => Explanation = string.Empty;

    public PatientHistoryVaccine(Guid patientHistoryId, Guid vaccineId, string? explanation)
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        VaccineId = Check.NotDefaultOrNull<Guid>(vaccineId, nameof(vaccineId));
        Explanation = Check.Length(explanation, nameof(explanation), VaccineConsts.ExplanationMaxLength);
    }

    public override object?[] GetKeys() => [PatientHistoryId, VaccineId];
}