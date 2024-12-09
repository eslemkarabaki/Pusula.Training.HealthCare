using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public class ProtocolType : AuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    protected ProtocolType() => Name = string.Empty;

    public ProtocolType(Guid id, string name) : base(id) => SetName(name);

    public void SetName(string name) => Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProtocolTypeConsts.NameMaxLength);
}