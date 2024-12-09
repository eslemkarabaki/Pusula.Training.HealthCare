using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols; 

namespace Pusula.Training.HealthCare.RadiologyRequests
{
    public class RadiologyRequestWithNavigationProperties
    {
        public RadiologyRequest RadiologyRequest { get; set; } = null!;
        public Protocol Protocol { get; set; } = null!;
        public Department Department { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
    }
}
