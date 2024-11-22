using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Cities;

public interface ICityAppService : IApplicationService
{
    Task<CityDto> GetAsync(Guid id);
    Task<CountryDto> GetCountryAsync(Guid cityId);
    Task<List<CityDto>> GetListAsync();
    Task<List<CityDto>> GetListAsync(Guid countryId);
    Task<PagedResultDto<CityDto>> GetListAsync(GetCitiesInput input);

    Task<CityDto> CreateAsync(CityCreateDto input);

    Task<CityDto> UpdateAsync(Guid id, CityUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> patientIds);
    Task DeleteAllAsync(GetCitiesInput input);
}