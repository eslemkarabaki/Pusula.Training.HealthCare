using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Cities;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Cities.Default)]
public class CityAppService(
    ICityRepository cityRepository,
    CityManager cityManager
)
    : HealthCareAppService, ICityAppService
{
    public async Task<CityDto> GetAsync(Guid id) => ObjectMapper.Map<City, CityDto>(await cityRepository.GetAsync(id));

    public async Task<List<CityDto>> GetListWithDetailsAsync(GetCitiesInput input) =>
        ObjectMapper.Map<List<City>, List<CityDto>>(
            await cityRepository.GetListWithDetailsAsync(input.FilterText, input.Name, input.CountryId)
        );

    [Authorize(HealthCarePermissions.Cities.Create)]
    public async Task<CityDto> CreateAsync(CityCreateDto input)
    {
        var city = await cityManager.CreateAsync(input.CountryId, input.Name);
        return ObjectMapper.Map<City, CityDto>(city);
    }

    [Authorize(HealthCarePermissions.Cities.Edit)]
    public async Task<CityDto> UpdateAsync(Guid id, CityUpdateDto input)
    {
        var city = await cityManager.UpdateAsync(id, input.CountryId, input.Name);
        return ObjectMapper.Map<City, CityDto>(city);
    }

    [Authorize(HealthCarePermissions.Cities.Delete)]
    public async Task DeleteAsync(Guid id) => await cityRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Cities.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> patientIds) => await cityRepository.DeleteManyAsync(patientIds);

    [Authorize(HealthCarePermissions.Cities.Delete)]
    public async Task DeleteAllAsync(GetCitiesInput input) =>
        await cityRepository.DeleteAllAsync(input.FilterText, input.Name, input.CountryId);
}