using Pusula.Training.HealthCare.Appointments;
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
        public Appointment Appointment { get; set; } = null!;
    }
}
