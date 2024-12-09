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
    ICityRepository cityRepository,
    DistrictManager districtManager
)
    : HealthCareAppService, IDistrictAppService
{
    public async Task<DistrictDto> GetAsync(Guid id) =>
        ObjectMapper.Map<District, DistrictDto>(await districtRepository.GetAsync(id));

    public async Task<CityDto> GetCityAsync(Guid districtId)
    {
        var district = await districtRepository.GetAsync(districtId);
        return ObjectMapper.Map<City, CityDto>(await cityRepository.GetAsync(e => e.Id == district.CityId));
    }

    public async Task<List<DistrictDto>> GetListWithDetailsAsync() =>
        ObjectMapper.Map<List<District>, List<DistrictDto>>(await districtRepository.GetListWithDetailsAsync());

    public async Task<List<DistrictDto>> GetListWithDetailsAsync(Guid cityId) =>
        ObjectMapper.Map<List<District>, List<DistrictDto>>(
            await districtRepository.GetListWithDetailsAsync(cityId: cityId)
        );

    public async Task<PagedResultDto<DistrictDto>> GetListWithDetailsAsync(GetDistrictsInput input)
    {
        var items = await districtRepository.GetListWithDetailsAsync(
            input.FilterText, input.Name, input.CityId,
            input.Sorting, input.MaxResultCount, input.SkipCount
        );

        var count = await districtRepository.GetCountAsync(input.FilterText, input.Name, input.CityId);

        return new PagedResultDto<DistrictDto>(
            count,
            ObjectMapper.Map<List<District>, List<DistrictDto>>(items)
        );
    }

    public async Task<DistrictDto> CreateAsync(DistrictCreateDto input)
    {
        var district = await districtManager.CreateAsync(input.CityId, input.Name);
        return ObjectMapper.Map<District, DistrictDto>(district);
    }

    public async Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input)
    {
        var district = await districtManager.UpdateAsync(id, input.CityId, input.Name);
        return ObjectMapper.Map<District, DistrictDto>(district);
    }

    public async Task DeleteAsync(Guid id) => await districtRepository.DeleteAsync(id);

    public async Task DeleteByIdsAsync(List<Guid> ids) => await districtRepository.DeleteManyAsync(ids);

    public async Task DeleteAllAsync(GetDistrictsInput input) =>
        await districtRepository.DeleteAllAsync(input.FilterText, input.Name, input.CityId);
}