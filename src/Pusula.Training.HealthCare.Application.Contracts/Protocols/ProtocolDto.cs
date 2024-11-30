using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp, IProtocol
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid ProtocolTypeId { get; set; }
    public EnumProtocolStatus Status { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}