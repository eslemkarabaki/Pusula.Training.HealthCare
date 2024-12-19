using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.Countries;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Countries.Default)]
public class CountryAppService(ICountryRepository countryRepository, CountryManager countryManager)
    : HealthCareAppService, ICountryAppService
{
    public async Task<CountryDto> GetAsync(Guid id) =>
        ObjectMapper.Map<Country, CountryDto>(await countryRepository.GetAsync(id));

    public async Task<CountryDto> GetCurrentAsync() =>
        ObjectMapper.Map<Country, CountryDto>(await countryRepository.GetAsync(e => e.IsCurrent));

    public async Task<List<CountryDto>> GetListAsync(GetCountriesInput input) =>
        ObjectMapper.Map<List<Country>, List<CountryDto>>(
            await countryRepository.GetListAsync(input.FilterText, input.Name, input.Iso, input.PhoneCode)
        );

    [Authorize(HealthCarePermissions.Countries.Create)]
    public async Task<CountryDto> CreateAsync(CountryCreateDto input)
    {
        var country = await countryManager.CreateAsync(input.Name, input.Iso, input.PhoneCode);
        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    [Authorize(HealthCarePermissions.Countries.Edit)]
    public async Task<CountryDto> UpdateAsync(Guid id, CountryUpdateDto input)
    {
        var country = await countryManager.UpdateAsync(id, input.Name, input.Iso, input.PhoneCode);
        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    [Authorize(HealthCarePermissions.Countries.Delete)]
    public async Task DeleteAsync(Guid id) => await countryRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Countries.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await countryRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.Countries.Delete)]
    public async Task DeleteAllAsync(GetCountriesInput input) =>
        await countryRepository.DeleteAllAsync(input.FilterText, input.Name, input.Iso, input.PhoneCode);
}