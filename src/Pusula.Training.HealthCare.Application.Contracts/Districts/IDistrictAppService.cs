using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Cities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Districts;

public interface IDistrictAppService : IApplicationService
{
    Task<DistrictDto> GetAsync(Guid id);
    Task<CityDto> GetCityAsync(Guid districtId);

    Task<List<DistrictDto>> GetListAsync();
    Task<List<DistrictDto>> GetListAsync(Guid cityId);
    Task<PagedResultDto<DistrictDto>> GetListAsync(GetDistrictsInput input);

    Task<DistrictDto> CreateAsync(DistrictCreateDto input);

    Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> patientIds);
    Task DeleteAllAsync(GetDistrictsInput input);
}