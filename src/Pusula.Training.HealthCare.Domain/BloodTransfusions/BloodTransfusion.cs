using System;
using System.Collections.Generic;
using Pusula.Training.HealthCare.PatientHistoryBloodTransfusions;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.BloodTransfusions;

public class BloodTransfusion : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }


    protected BloodTransfusion()
    {
        Name = string.Empty;
    }

    public BloodTransfusion(Guid id, string name) : base(id)
    {
        SetName(name);
    }

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), BloodTransfusionConsts.NameMaxLength);
}