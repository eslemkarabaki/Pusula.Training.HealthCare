using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Protocols;

public class GetDoctorWorkListInput(Guid userId): PagedAndSortedResultRequestDto
{
    public Guid UserId { get; set; } = userId;
    public EnumProtocolStatus Status { get; set; } = EnumProtocolStatus.None;
    public DateTime StartTime { get; set; } = DateTime.Today;
    public DateTime EndTime { get; set; }
}