using System; 
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItemDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid? RequestId { get; set; }
    public Guid? ExaminationId { get; set; }
    public string Result { get; set; }
    public DateTime? ResultDate { get; set; }
    public RadiologyRequestItemState? State { get; set; } = RadiologyRequestItemState.Pending;
    public string ConcurrencyStamp { get; set; } = null!;
}
