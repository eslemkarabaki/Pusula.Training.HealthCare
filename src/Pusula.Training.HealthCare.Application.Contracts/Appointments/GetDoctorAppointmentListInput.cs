using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Appointments;

public class GetDoctorAppointmentListInput(Guid doctorId) : PagedAndSortedResultRequestDto
{
    public Guid DoctorId { get; set; } = doctorId;
    public DateTime StartTime { get; set; } = DateTime.Today;
    public DateTime EndTime { get; set; }
    public ICollection<EnumAppointmentStatus> Status { get; set; } = [];

    public bool HasStatus(EnumAppointmentStatus status) => Status.Contains(status);

    public void ToggleStatus(EnumAppointmentStatus status)
    {
        if (HasStatus(status))
            Status.Remove(status);
        else
            Status.Add(status);
    }
}