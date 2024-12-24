using System;
using Pusula.Training.HealthCare.BloodTransfusions;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryBloodTransfusions;

public class PatientHistoryBloodTransfusion : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid BloodTransfusionId { get; set; }
    public virtual BloodTransfusion BloodTransfusion { get; set; }
    public string? Explanation { get; set; }

    protected PatientHistoryBloodTransfusion() => Explanation = string.Empty;

    public PatientHistoryBloodTransfusion(
        Guid patientHistoryId,
        Guid bloodTransfusionId,
        string? explanation
    )
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        BloodTransfusionId = Check.NotDefaultOrNull<Guid>(bloodTransfusionId, nameof(bloodTransfusionId));
        Explanation = Check.Length(
            explanation, nameof(explanation), BloodTransfusionConsts.ExplanationMaxLength
        );
    }

    public override object?[] GetKeys() => [PatientHistoryId, BloodTransfusionId];
}