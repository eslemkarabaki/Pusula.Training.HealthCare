using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public class ProtocolType : AuditedEntity<Guid>
{
    public string Name { get; set; }

    protected ProtocolType() => Name = string.Empty;

    public ProtocolType(Guid id, string name) : base(id) => SetName(name);

    public void SetName(string name) => Check.NotNullOrWhiteSpace(name, nameof(name), ProtocolTypeConsts.NameMaxLength);
}