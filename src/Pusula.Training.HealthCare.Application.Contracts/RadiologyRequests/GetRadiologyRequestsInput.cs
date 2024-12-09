using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.RadiologyRequests;
public class GetRadiologyRequestsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public DateTime RequestDate { get; set; }
    public Guid ProtocolId { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid DoctorId { get; set; }

    public GetRadiologyRequestsInput() { }
}