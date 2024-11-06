using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Countries;

namespace Pusula.Training.HealthCare.Patients;

public class PatientWithNavigationPropertiesDto
{
    public PatientDto Patient { get; set; } = null!;
    public AddressWithNavigationPropertiesDto Address { get; set; } = null!;
    public CountryDto Country { get; set; } = null!;
}