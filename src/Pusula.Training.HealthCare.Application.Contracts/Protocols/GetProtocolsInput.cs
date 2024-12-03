using Volo.Abp.Application.Dtos;
using System;

namespace Pusula.Training.HealthCare.Protocols;

public class GetProtocolsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public Guid? PatientId { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? ProtocolTypeId { get; set; }
    public EnumProtocolStatus Status { get; set; } = EnumProtocolStatus.None;
    public DateTime StartTime { get; set; } = DateTime.Today;
    public DateTime EndTime { get; set; }

    public GetProtocolsInput() { }
}