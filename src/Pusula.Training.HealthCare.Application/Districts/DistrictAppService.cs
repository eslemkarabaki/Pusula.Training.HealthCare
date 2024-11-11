using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Districts;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Districts.Default)]
public class DistrictAppService(IDistrictRepository districtRepository, DistrictManager districtManager)
    : HealthCareAppService, IDistrictAppService
{
    public async Task<DistrictDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<District, DistrictDto>(await districtRepository.GetAsync(id));
    }

    public async Task<List<DistrictDto>> GetListAsync()
    {
        return ObjectMapper.Map<List<DistrictWithCity>, List<DistrictDto>>(await districtRepository.GetListAsync());
    }

    public async Task<List<DistrictDto>> GetListAsync(Guid cityId)
    {
        return ObjectMapper.Map<List<District>, List<DistrictDto>>(
            await districtRepository.GetListAsync(e => e.CityId == cityId));
    }

    public async Task<PagedResultDto<DistrictDto>> GetListAsync(GetDistrictsInput input)
    {
        var items = await districtRepository.GetListAsync(input.FilterText, input.Name, input.CityId,
            input.Sorting, input.MaxResultCount, input.SkipCount);

        var count = await districtRepository.GetCountAsync(input.FilterText, input.Name, input.CityId);

        return new PagedResultDto<DistrictDto>(count,
            ObjectMapper.Map<List<DistrictWithCity>, List<DistrictDto>>(items));
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

    public async Task DeleteAsync(Guid id)
    {
        await districtRepository.DeleteAsync(id);
    }

    public async Task DeleteByIdsAsync(List<Guid> ids)
    {
        await districtRepository.DeleteManyAsync(ids);
    }

    public async Task DeleteAllAsync(GetDistrictsInput input)
    {
        await districtRepository.DeleteAllAsync(input.FilterText, input.Name, input.CityId);
    }
}