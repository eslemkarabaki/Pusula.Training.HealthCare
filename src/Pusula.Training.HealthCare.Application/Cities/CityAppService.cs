using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Cities;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Cities.Default)]
public class CityAppService(ICityRepository cityRepository) : HealthCareAppService, ICityAppService
{
    public async Task<IEnumerable<CityDto>> GetListAsync(Guid countryId)
    {
        return ObjectMapper.Map<List<City>, List<CityDto>>(
            await cityRepository.GetListAsync(e => e.CountryId == countryId));
    }
}