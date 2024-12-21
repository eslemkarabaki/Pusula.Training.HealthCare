using System;
using Pusula.Training.HealthCare.Operations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientHistoryOperations;

public class PatientHistoryOperation : Entity
{
    public Guid PatientHistoryId { get; set; }
    public Guid OperationId { get; set; }
    public string Explanation { get; set; }
    public bool NotExist { get; set; }

    protected PatientHistoryOperation() => Explanation = string.Empty;

    public PatientHistoryOperation(Guid patientHistoryId, Guid operationId, string explanation, bool notExist)
    {
        PatientHistoryId = Check.NotDefaultOrNull<Guid>(patientHistoryId, nameof(patientHistoryId));
        OperationId = Check.NotDefaultOrNull<Guid>(operationId, nameof(operationId));
        Explanation = Check.NotNullOrWhiteSpace(explanation, nameof(explanation), OperationConsts.ExplanationMaxLength);
        NotExist = notExist;
    }

    public override object?[] GetKeys() => [PatientHistoryId, OperationId];
}