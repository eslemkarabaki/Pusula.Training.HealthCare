using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class GetAppointmentReportsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public DateTime? ReportDate { get; set; }
        public string? PriorityNotes { get; set; } = null!;
        public string? DoctorNotes { get; set; } = null!;
        public Guid? AppointmentId { get; set; }

        public GetAppointmentReportsInput() { 
        }
    }
}
