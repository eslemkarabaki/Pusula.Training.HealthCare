using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Cities;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Cities.Default)]
public class CityAppService(ICityRepository cityRepository, CityManager cityManager)
    : HealthCareAppService, ICityAppService
{
    public async Task<CityDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<City, CityDto>(await cityRepository.GetAsync(id));
    }

    public async Task<List<CityDto>> GetListAsync()
    {
        return ObjectMapper.Map<List<CityWithCountry>, List<CityDto>>(
            await cityRepository.GetListAsync());
    }

    public async Task<List<CityDto>> GetListAsync(Guid countryId)
    {
        return ObjectMapper.Map<List<City>, List<CityDto>>(
            await cityRepository.GetListAsync(e => e.CountryId == countryId));
    }

    public async Task<PagedResultDto<CityDto>> GetListAsync(GetCitiesInput input)
    {
        var items = await cityRepository.GetListAsync(input.FilterText, input.Name, input.CountryId, input.Sorting,
            input.MaxResultCount, input.SkipCount);

        var count = await cityRepository.GetCountAsync(input.FilterText, input.Name, input.CountryId);

        return new PagedResultDto<CityDto>(count, ObjectMapper.Map<List<CityWithCountry>, List<CityDto>>(items));
    }

    public async Task<CityDto> CreateAsync(CityCreateDto input)
    {
        var city = await cityManager.CreateAsync(input.CountryId, input.Name);
        return ObjectMapper.Map<City, CityDto>(city);
    }

    public async Task<CityDto> UpdateAsync(Guid id, CityUpdateDto input)
    {
        var city = await cityManager.UpdateAsync(id, input.CountryId, input.Name);
        return ObjectMapper.Map<City, CityDto>(city);
    }

    public async Task DeleteAsync(Guid id)
    {
        await cityRepository.DeleteAsync(id);
    }

    public async Task DeleteByIdsAsync(List<Guid> patientIds)
    {
        await cityRepository.DeleteManyAsync(patientIds);
    }

    public async Task DeleteAllAsync(GetCitiesInput input)
    {
        await cityRepository.DeleteAllAsync(input.FilterText, input.Name, input.CountryId);
    }
}