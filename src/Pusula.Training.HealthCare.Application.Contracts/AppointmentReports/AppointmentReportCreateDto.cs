using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Guid AppointmentId { get; set; }

    }
}
