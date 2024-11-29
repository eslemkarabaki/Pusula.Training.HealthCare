using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Diagnoses;

public sealed class Diagnosis : FullAuditedAggregateRoot<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }

    private Diagnosis()
    {
        Code = string.Empty;
        Name = string.Empty;
    }

    public Diagnosis(Guid id, string code, string name)
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotNullOrWhiteSpace(code, nameof(code), DiagnosisConsts.CodeMaxLength);
        Check.NotNullOrWhiteSpace(name, nameof(name), DiagnosisConsts.NameMaxLength);

        Id = id;
        Code = code;
        Name = name;
    }
}