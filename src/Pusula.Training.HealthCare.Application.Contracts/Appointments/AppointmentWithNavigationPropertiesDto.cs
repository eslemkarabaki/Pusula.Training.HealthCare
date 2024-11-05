using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentWithNavigationPropertiesDto
    {
        public AppointmentDto Appointment { get; set; } = null!;
        public HospitalDto Hospital { get; set; } = null!;
        public DepartmentDto Department { get; set; } = null!;
        public DoctorDto Doctor { get; set; }= null!;
        public PatientDto Patient { get; set; } = null!;

    }
}
