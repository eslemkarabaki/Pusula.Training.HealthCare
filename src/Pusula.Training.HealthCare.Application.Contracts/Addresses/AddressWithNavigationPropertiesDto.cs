using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressWithNavigationPropertiesDto
{
    public AddressDto Address { get; set; }
    public CountryDto Country { get; set; }
    public CityDto City { get; set; }
    public DistrictDto District { get; set; }
}