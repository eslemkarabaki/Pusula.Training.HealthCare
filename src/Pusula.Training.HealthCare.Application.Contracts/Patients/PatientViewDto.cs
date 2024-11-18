using System.Collections.Generic;
using Pusula.Training.HealthCare.Addresses;

namespace Pusula.Training.HealthCare.Patients;

public class PatientViewDto : PatientDto
{
    public string Country { get; set; } = null!;
    public string PatientType { get; set; } = null!;
    public IEnumerable<AddressDto> Addresses { get; set; } = [];
}