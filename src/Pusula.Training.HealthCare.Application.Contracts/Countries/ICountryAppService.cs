using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Countries;

public interface ICountryAppService : IApplicationService
{
    Task<CountryDto> GetAsync(Guid id);
    Task<CountryDto> GetCurrentAsync();

    Task<List<CountryDto>> GetListAsync(GetCountriesInput input);

    Task<CountryDto> CreateAsync(CountryCreateDto input);

    Task<CountryDto> UpdateAsync(Guid id, CountryUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> patientIds);
    Task DeleteAllAsync(GetCountriesInput input);
}