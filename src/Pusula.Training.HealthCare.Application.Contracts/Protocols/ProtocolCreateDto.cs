using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolCreateDto
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid ProtocolTypeId { get; set; }
    public EnumProtocolStatus Status { get; set; } = EnumProtocolStatus.InProgress;
    public string? Description { get; set; }
}