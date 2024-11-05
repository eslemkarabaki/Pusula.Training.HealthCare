using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentCreateDto
    {
        public DateTime AppointmentDate { get; set; } 
        public EnumStatus Status { get; set; }
        public string Notes { get; set; }
        public Guid HospitalId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
    }
}
