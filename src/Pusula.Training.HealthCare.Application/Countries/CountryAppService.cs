using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Countries;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Countries.Default)]
public class CountryAppService(ICountryRepository countryRepository) : HealthCareAppService, ICountryAppService
{
    public async Task<IEnumerable<CountryDto>> GetListAsync()
    {
        return ObjectMapper.Map<List<Country>, List<CountryDto>>(await countryRepository.GetListAsync());
    }
}