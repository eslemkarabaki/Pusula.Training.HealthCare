using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Vaccines;

public interface IVaccineAppService : IApplicationService
{
    Task<VaccineDto> GetAsync(Guid id);
    Task<List<VaccineDto>> GetListAsync(GetVaccinesInput input);
    Task<VaccineDto> CreateAsync(VaccineCreateDto input);

    Task<VaccineDto> UpdateAsync(Guid id, VaccineUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> ids);
    Task DeleteAllAsync(GetVaccinesInput input);
}