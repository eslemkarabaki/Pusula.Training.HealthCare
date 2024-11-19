using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class AppointmentReportWithNavigationProperties
    {
        public AppointmentReport AppointmentReport { get; set; } = null!;
        public AppointmentWithNavigationProperties Appointment { get; set; } = null!;

        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
    }
}
