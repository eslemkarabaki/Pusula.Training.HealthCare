using System;
using Pusula.Training.HealthCare.BloodTransfusions;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryBloodTransfusions;

public class PatientHistoryBloodTransfusion : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid BloodTransfusionId { get; set; }
    public string Explanation { get; set; }
    public bool NotExist { get; set; }

    protected PatientHistoryBloodTransfusion() => Explanation = string.Empty;

    public PatientHistoryBloodTransfusion(
        Guid patientHistoryId,
        Guid bloodTransfusionId,
        string explanation,
        bool notExist
    )
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        BloodTransfusionId = Check.NotDefaultOrNull<Guid>(bloodTransfusionId, nameof(bloodTransfusionId));
        Explanation = Check.NotNullOrWhiteSpace(
            explanation, nameof(explanation), BloodTransfusionConsts.ExplanationMaxLength
        );
        NotExist = notExist;
    }

    public override object?[] GetKeys() => [PatientHistoryId, BloodTransfusionId];
}