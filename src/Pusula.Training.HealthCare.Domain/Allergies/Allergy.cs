using System;
using System.Collections.Generic;
using Pusula.Training.HealthCare.PatientHistoryAllergies;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Allergies;

public class Allergy : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    protected Allergy()
    {
        Name = string.Empty;
    }

    public Allergy(Guid id, string name) : base(id)
    {
        SetName(name);
    }

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), AllergyConsts.NameMaxLength);
}