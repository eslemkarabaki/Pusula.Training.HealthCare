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
    ICountryRepository countryRepository,
    CityManager cityManager
)
    : HealthCareAppService, ICityAppService
{
    public async Task<CityDto> GetAsync(Guid id) => ObjectMapper.Map<City, CityDto>(await cityRepository.GetAsync(id));

    public async Task<CountryDto> GetCountryAsync(Guid cityId)
    {
        var city = await cityRepository.GetAsync(cityId);
        return ObjectMapper.Map<Country, CountryDto>(await countryRepository.GetAsync(e => e.Id == city.CountryId));
    }

    public async Task<List<CityDto>> GetListWithDetailsAsync() =>
        ObjectMapper.Map<List<City>, List<CityDto>>(
            await cityRepository.GetListWithDetailsAsync()
        );

    public async Task<List<CityDto>> GetListWithDetailsAsync(Guid countryId) =>
        ObjectMapper.Map<List<City>, List<CityDto>>(
            await cityRepository.GetListWithDetailsAsync(countryId: countryId)
        );

    public async Task<PagedResultDto<CityDto>> GetListWithDetailsAsync(GetCitiesInput input)
    {
        var items = await cityRepository.GetListWithDetailsAsync(
            input.FilterText, input.Name, input.CountryId, input.Sorting,
            input.MaxResultCount, input.SkipCount
        );

        var count = await cityRepository.GetCountAsync(input.FilterText, input.Name, input.CountryId);

        return new PagedResultDto<CityDto>(count, ObjectMapper.Map<List<City>, List<CityDto>>(items));
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

    public async Task DeleteAsync(Guid id) => await cityRepository.DeleteAsync(id);

    public async Task DeleteByIdsAsync(List<Guid> patientIds) => await cityRepository.DeleteManyAsync(patientIds);

    public async Task DeleteAllAsync(GetCitiesInput input) =>
        await cityRepository.DeleteAllAsync(input.FilterText, input.Name, input.CountryId);
}