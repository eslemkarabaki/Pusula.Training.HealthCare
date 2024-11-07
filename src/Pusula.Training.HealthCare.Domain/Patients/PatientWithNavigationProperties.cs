using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Countries;

namespace Pusula.Training.HealthCare.Patients;

public class PatientWithNavigationProperties
{
    public Patient Patient { get; set; } = null!;
    public AddressWithNavigationProperties Address { get; set; } = null!;
    public Country Country { get; set; } = null!;
}