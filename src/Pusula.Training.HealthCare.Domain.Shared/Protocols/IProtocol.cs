using System;

namespace Pusula.Training.HealthCare.Protocols;

public interface IProtocol
{
    public Guid Id { get; }
    public Guid PatientId { get; }
    public Guid DoctorId { get; }
    public Guid DepartmentId { get; }
    public Guid ProtocolTypeId { get; }
    public EnumProtocolStatus Status { get; }
    public string? Description { get; }
    public DateTime StartTime { get; }
    public DateTime? EndTime { get; }
}