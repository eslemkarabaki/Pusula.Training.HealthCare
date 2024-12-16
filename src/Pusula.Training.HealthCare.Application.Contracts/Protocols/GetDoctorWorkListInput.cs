using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Protocols;

public class GetDoctorWorkListInput(Guid userId) : PagedAndSortedResultRequestDto
{
    public Guid UserId { get; set; } = userId;
    public ICollection<EnumProtocolStatus> Status { get; set; } = [];
    public DateTime StartTime { get; set; } = DateTime.Today;
    public DateTime EndTime { get; set; }

    public bool HasStatus(EnumProtocolStatus status) => Status.Contains(status);

    public void ToggleStatus(EnumProtocolStatus status)
    {
        if (HasStatus(status))
        {
            Status.Remove(status);
        } else
        {
            Status.Add(status);
        }
    }
}