using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class AppointmentReportCreateDto
    {
        [Required]
        public DateTime ReportDate { get; set; }
        [StringLength(AppointmentReportConsts.PriorityNotesMaxLength)]
        public string PriorityNotes { get; set; } = null!;
        [StringLength(AppointmentReportConsts.DoctorNotesMaxLength)]
        public string DoctorNotes { get; set; } = null!;
        [Required]
        public Guid AppointmentId { get; set; }

    }
}
