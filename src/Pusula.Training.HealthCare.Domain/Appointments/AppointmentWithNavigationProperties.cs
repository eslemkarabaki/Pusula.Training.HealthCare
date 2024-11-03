using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentWithNavigationProperties
    {
        public Hospital Hospital { get; set; }
        public Department Department { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
