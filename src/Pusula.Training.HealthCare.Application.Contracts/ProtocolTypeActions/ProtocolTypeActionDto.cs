using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public class ProtocolTypeActionDto : FullAuditedEntityDto<Guid>
{
    public Guid ProtocolTypeId { get; set; }
    public string Name { get; set; } = null!;
}