using System; 
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadiologyRequests;
public class RadiologyRequestDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public DateTime RequestDate { get; set; }
    public Guid ProtocolId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid DoctorId { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;
}
