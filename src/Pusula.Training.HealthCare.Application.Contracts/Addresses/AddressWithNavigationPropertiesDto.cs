using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressWithNavigationPropertiesDto
{
    public AddressDto Address { get; set; } = null!;
    public CountryDto Country { get; set; } = null!;
    public CityDto City { get; set; } = null!;
    public DistrictDto District { get; set; } = null!;
}