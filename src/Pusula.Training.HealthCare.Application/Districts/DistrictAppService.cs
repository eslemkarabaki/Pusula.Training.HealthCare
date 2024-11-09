using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Districts;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Districts.Default)]
public class DistrictAppService(IDistrictRepository districtRepository) : HealthCareAppService, IDistrictAppService
{
    public async Task<IEnumerable<DistrictDto>> GetListAsync(Guid cityId)
    {
        return ObjectMapper.Map<List<District>, List<DistrictDto>>(
            await districtRepository.GetListAsync(e => e.CityId == cityId));
    }
}