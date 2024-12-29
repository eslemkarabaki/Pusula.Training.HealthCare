using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PatientTypes;

public class PatientType : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    protected PatientType() => Name = string.Empty;

    public PatientType(Guid id, string name) : base(id) => SetName(name);

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), PatientTypeContst.NameMaxLength);
}