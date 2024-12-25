using System;
using Pusula.Training.HealthCare.Operations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryOperations;

public class PatientHistoryOperation : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid OperationId { get; set; }
    public virtual Operation Operation { get; set; }
    public string? Explanation { get; set; }

    protected PatientHistoryOperation() => Explanation = string.Empty;

    public PatientHistoryOperation(Guid patientHistoryId, Guid operationId, string? explanation)
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        OperationId = Check.NotDefaultOrNull<Guid>(operationId, nameof(operationId));
        Explanation = Check.Length(explanation, nameof(explanation), OperationConsts.ExplanationMaxLength);
    }

    public override object?[] GetKeys() => [PatientHistoryId, OperationId];
}