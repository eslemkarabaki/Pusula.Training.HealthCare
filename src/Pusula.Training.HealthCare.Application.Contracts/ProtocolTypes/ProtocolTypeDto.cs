using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public class ProtocolTypeDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = null!;
}