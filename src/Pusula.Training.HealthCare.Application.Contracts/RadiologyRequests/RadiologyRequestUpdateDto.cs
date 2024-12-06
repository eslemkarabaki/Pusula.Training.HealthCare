using System; 
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.RadiologyRequests;
public class RadiologyRequestUpdateDto : IHasConcurrencyStamp
{
    public DateTime RequestDate { get; set; }
    public Guid ProtocolId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid DoctorId { get; set; }
    public string ConcurrencyStamp { get; set; }
} 