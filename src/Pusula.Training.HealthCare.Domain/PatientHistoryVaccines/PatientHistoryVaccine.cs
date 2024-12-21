using System;
using Pusula.Training.HealthCare.Vaccines;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryVaccines;

public class PatientHistoryVaccine : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid VaccineId { get; set; }
    public string Explanation { get; set; }
    public bool NotExist { get; set; }

    protected PatientHistoryVaccine() => Explanation = string.Empty;

    public PatientHistoryVaccine(Guid patientHistoryId, Guid vaccineId, string explanation, bool notExist)
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        VaccineId = Check.NotDefaultOrNull<Guid>(vaccineId, nameof(vaccineId));
        Explanation = Check.NotNullOrWhiteSpace(explanation, nameof(explanation), VaccineConsts.ExplanationMaxLength);
        NotExist = notExist;
    }

    public override object?[] GetKeys() => [PatientHistoryId, VaccineId];
}