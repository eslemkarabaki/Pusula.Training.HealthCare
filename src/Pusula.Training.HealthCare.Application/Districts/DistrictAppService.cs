using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Districts;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Districts.Default)]
public class DistrictAppService(
    IDistrictRepository districtRepository,
    DistrictManager districtManager
)
    : HealthCareAppService, IDistrictAppService
{
    public async Task<DistrictDto> GetAsync(Guid id) =>
        ObjectMapper.Map<District, DistrictDto>(await districtRepository.GetAsync(id));

    public async Task<List<DistrictDto>> GetListWithDetailsAsync(GetDistrictsInput input) =>
        ObjectMapper.Map<List<District>, List<DistrictDto>>(
            await districtRepository.GetListWithDetailsAsync(input.FilterText, input.Name, input.CityId)
        );

    [Authorize(HealthCarePermissions.Districts.Create)]
    public async Task<DistrictDto> CreateAsync(DistrictCreateDto input)
    {
        var district = await districtManager.CreateAsync(input.CityId, input.Name);
        return ObjectMapper.Map<District, DistrictDto>(district);
    }

    [Authorize(HealthCarePermissions.Districts.Edit)]
    public async Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input)
    {
        var district = await districtManager.UpdateAsync(id, input.CityId, input.Name);
        return ObjectMapper.Map<District, DistrictDto>(district);
    }

    [Authorize(HealthCarePermissions.Districts.Delete)]
    public async Task DeleteAsync(Guid id) => await districtRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Districts.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await districtRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.Districts.Delete)]
    public async Task DeleteAllAsync(GetDistrictsInput input) =>
        await districtRepository.DeleteAllAsync(input.FilterText, input.Name, input.CityId);
}