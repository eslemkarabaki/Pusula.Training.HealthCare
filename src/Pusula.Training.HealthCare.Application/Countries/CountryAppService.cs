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
    public async Task<CountryDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Country, CountryDto>(await countryRepository.GetAsync(id));
    }

    public async Task<List<CountryDto>> GetListAsync()
    {
        return ObjectMapper.Map<List<Country>, List<CountryDto>>(await countryRepository.GetListAsync());
    }

    public async Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input)
    {
        var items = await countryRepository.GetListAsync(input.FilterText, input.Name, input.Abbreviation,
            input.Sorting, input.MaxResultCount, input.SkipCount);

        var count = await countryRepository.GetCountAsync(input.FilterText, input.Name, input.Abbreviation);

        return new PagedResultDto<CountryDto>(count, ObjectMapper.Map<List<Country>, List<CountryDto>>(items));
    }

    public async Task<CountryDto> CreateAsync(CountryCreateDto input)
    {
        var country = await countryManager.CreateAsync(input.Name, input.Abbreviation);
        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    public async Task<CountryDto> UpdateAsync(Guid id, CountryUpdateDto input)
    {
        var country = await countryManager.UpdateAsync(id, input.Name, input.Abbreviation);
        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    public async Task DeleteAsync(Guid id)
    {
        await countryRepository.DeleteAsync(id);
    }

    public async Task DeleteByIdsAsync(List<Guid> ids)
    {
        await countryRepository.DeleteManyAsync(ids);
    }

    public async Task DeleteAllAsync(GetCountriesInput input)
    {
        await countryRepository.DeleteAllAsync(input.FilterText, input.Name, input.Abbreviation);
    }
}