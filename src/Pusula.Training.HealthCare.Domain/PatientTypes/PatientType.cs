using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PatientTypes;

public sealed class PatientType : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    private PatientType()
    {
        Name = string.Empty;
    }

    internal PatientType(Guid id, string name) : base(id)
    {
        Set(name);
    }

    internal void Set(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), PatientTypeContst.NameMaxLength);
    }
}