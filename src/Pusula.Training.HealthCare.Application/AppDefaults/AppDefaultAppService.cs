using System;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Countries;

namespace Pusula.Training.HealthCare.AppDefaults;

public class AppDefaultAppService(IAppDefaultRepository appDefaultRepository, AppDefaultManager appDefaultManager)
    : HealthCareAppService, IAppDefaultAppService
{
    public async Task<CountryDto> GetCurrentCountryAsync()
    {
        var country = await appDefaultRepository.GetCurrentCountryAsync();
        return country == null ? new CountryDto() : ObjectMapper.Map<Country, CountryDto>(country);
    }

    public async Task SetCurrentCountryAsync(Guid countryId) => await appDefaultManager.SetCurrentCountry(countryId);
}