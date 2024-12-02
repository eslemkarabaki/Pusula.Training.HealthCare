using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class AppointmentReportWithNavigationProperties
    {
        public AppointmentReport AppointmentReport { get; set; } = null!;
        public Appointment Appointment { get; set; } = null!;

        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
    }
}
