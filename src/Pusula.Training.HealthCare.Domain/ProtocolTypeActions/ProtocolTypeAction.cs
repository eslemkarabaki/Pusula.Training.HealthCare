using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public class ProtocolTypeAction : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public Guid ProtocolTypeId { get; private set; }

    protected ProtocolTypeAction() => Name = string.Empty;

    public ProtocolTypeAction(Guid id, string name, Guid protocolTypeId) : base(id)
    {
        SetName(name);
        SetProtocolTypeId(protocolTypeId);
    }

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProtocolTypeActionConsts.NameMaxLength);

    public void SetProtocolTypeId(Guid protocolTypeId) =>
        ProtocolTypeId = Check.NotDefaultOrNull<Guid>(protocolTypeId, nameof(protocolTypeId));
}