using System;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.ProtocolTypeActions;
using Pusula.Training.HealthCare.ProtocolTypes;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid PatientId { get; set; }
    public PatientDto Patient { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public DoctorDto Doctor { get; set; } = null!;

    public Guid DepartmentId { get; set; }
    public DepartmentDto Department { get; set; } = null!;

    public Guid ProtocolTypeId { get; set; }
    public ProtocolTypeDto ProtocolType { get; set; } = null!;

    public Guid ProtocolTypeActionId { get; set; }
    public ProtocolTypeActionDto ProtocolTypeAction { get; set; } = null!;

    public EnumProtocolStatus Status { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}