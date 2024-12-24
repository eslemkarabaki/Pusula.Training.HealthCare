using System;
using Pusula.Training.HealthCare.Allergies;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryAllergies;

public class PatientHistoryAllergy : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid AllergyId { get; set; }
    public virtual Allergy Allergy { get; set; }
    public string? Explanation { get; set; }

    protected PatientHistoryAllergy() => Explanation = string.Empty;

    public PatientHistoryAllergy(Guid patientHistoryId, Guid allergyId, string? explanation)
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        AllergyId = Check.NotDefaultOrNull<Guid>(allergyId, nameof(allergyId));
        Explanation = Check.Length(explanation, nameof(explanation), AllergyConsts.ExplanationMaxLength);
    }

    public override object?[] GetKeys() => [PatientHistoryId, AllergyId];
}