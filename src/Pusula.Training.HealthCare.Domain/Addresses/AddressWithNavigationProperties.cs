using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressWithNavigationProperties
{
    public Address Address { get; set; } = null!;
    public Country Country { get; set; } = null!;
    public City City { get; set; } = null!;
    public District District { get; set; } = null!;
}