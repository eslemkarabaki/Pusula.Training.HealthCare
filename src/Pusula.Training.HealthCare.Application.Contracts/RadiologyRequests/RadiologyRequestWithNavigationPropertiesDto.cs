using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols;

namespace Pusula.Training.HealthCare.RadiologyRequests;
public class RadiologyRequestWithNavigationPropertiesDto
{
    public RadiologyRequestDto RadiologyRequest { get; set; } = null!;
    public ProtocolDto Protocol { get; set; } = null!;
    public DepartmentDto Department { get; set; } = null!;
    public DoctorDto Doctor { get; set; } = null!;
}

