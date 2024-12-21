using System;
using System.Collections.Generic;
using Pusula.Training.HealthCare.PatientHistoryVaccines;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Vaccines;

public class Vaccine : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    public ICollection<PatientHistoryVaccine> Vaccines { get; set; }

    protected Vaccine()
    {
        Name = string.Empty;
        Vaccines = [];
    }

    public Vaccine(Guid id, string name) : base(id)
    {
        SetName(name);
        Vaccines = [];
    }

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), VaccineConsts.NameMaxLength);
}