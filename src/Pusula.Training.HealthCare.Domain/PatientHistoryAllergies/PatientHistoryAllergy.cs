using System;
using Pusula.Training.HealthCare.Allergies;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryAllergies;

public class PatientHistoryAllergy : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid AllergyId { get; set; }
    public string Explanation { get; set; }
    public bool NotExist { get; set; }

    protected PatientHistoryAllergy() => Explanation = string.Empty;

    public PatientHistoryAllergy(Guid patientHistoryId, Guid allergyId, string explanation, bool notExist)
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        AllergyId = Check.NotDefaultOrNull<Guid>(allergyId, nameof(allergyId));
        Explanation = Check.NotNullOrWhiteSpace(explanation, nameof(explanation), AllergyConsts.ExplanationMaxLength);
        NotExist = notExist;
    }

    public override object?[] GetKeys() => [PatientHistoryId, AllergyId];
}