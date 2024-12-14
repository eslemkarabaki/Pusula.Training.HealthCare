using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public class ProtocolTypeActionDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid ProtocolTypeId { get; set; }
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}