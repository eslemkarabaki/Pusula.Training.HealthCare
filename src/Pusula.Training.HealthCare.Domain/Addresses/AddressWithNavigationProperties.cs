using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressWithNavigationProperties
{
    public Address Address { get; set; }
    public Country Country { get; set; }
    public City City { get; set; }
    public District District { get; set; }
}