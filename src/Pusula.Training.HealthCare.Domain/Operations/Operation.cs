using System;
using System.Collections.Generic;
using Pusula.Training.HealthCare.PatientHistoryOperations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Operations;

public class Operation : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    protected Operation()
    {
        Name = string.Empty;
    }

    public Operation(Guid id, string name) : base(id)
    {
        SetName(name);
    }

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), OperationConsts.NameMaxLength);
}