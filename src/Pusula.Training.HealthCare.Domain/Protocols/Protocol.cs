using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Protocols;

public sealed class Protocol : FullAuditedAggregateRoot<Guid>, IProtocol
{
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid ProtocolTypeId { get; private set; }
    public EnumProtocolStatus Status { get; private set; }
    public string? Description { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }

    protected Protocol() => Description = string.Empty;

    public Protocol(
        Guid id,
        Guid patientId,
        Guid doctorId,
        Guid departmentId,
        Guid typeId,
        string? description,
        EnumProtocolStatus status,
        DateTime startTime,
        DateTime? endTime = null
    ) : base(id)
    {
        SetPatientId(patientId);
        SetDoctorId(doctorId);
        SetDepartmentId(departmentId);
        SetTypeId(typeId);
        SetDescription(description);
        SetStatus(status);
        SetStartTime(startTime);
        SetEndTime(endTime);
    }

    public void SetPatientId(Guid patientId) => Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
    public void SetDoctorId(Guid doctorId) => Check.NotDefaultOrNull<Guid>(doctorId, nameof(doctorId));
    public void SetDepartmentId(Guid departmentId) => Check.NotDefaultOrNull<Guid>(departmentId, nameof(departmentId));
    public void SetTypeId(Guid typeId) => Check.NotDefaultOrNull<Guid>(typeId, nameof(typeId));
    public void SetStatus(EnumProtocolStatus status) => Status = status;

    public void SetDescription(string? description) =>
        Check.Length(description, nameof(description), ProtocolConsts.DescriptionMaxLength);

    public void SetStartTime(DateTime startTime) => StartTime = startTime;
    public void SetEndTime(DateTime? endTime) => EndTime = endTime;
}