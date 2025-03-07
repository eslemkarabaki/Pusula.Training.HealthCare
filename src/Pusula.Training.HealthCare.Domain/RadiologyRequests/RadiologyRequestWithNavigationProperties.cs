﻿using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.ProtocolTypes;

namespace Pusula.Training.HealthCare.RadiologyRequests
{
    public class RadiologyRequestWithNavigationProperties
    {
        public RadiologyRequest RadiologyRequest { get; set; } = null!;
        public Protocol Protocol { get; set; } = null!;
        public Department Department { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;
        public ProtocolType ProtocolType { get; set; } = null!;
        public Patient Patient { get; set; } = null!;
    }
}
