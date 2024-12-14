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

    Task<List<DistrictDto>> GetListWithDetailsAsync(GetDistrictsInput input);

    Task<DistrictDto> CreateAsync(DistrictCreateDto input);

    Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> patientIds);
    Task DeleteAllAsync(GetDistrictsInput input);
}