using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.BloodTransfusions;

public interface IBloodTransfusionAppService : IApplicationService
{
    Task<BloodTransfusionDto> GetAsync(Guid id);
    Task<List<BloodTransfusionDto>> GetListAsync(GetBloodTransfusionsInput input);
    Task<BloodTransfusionDto> CreateAsync(BloodTransfusionCreateDto input);

    Task<BloodTransfusionDto> UpdateAsync(Guid id, BloodTransfusionUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> ids);
    Task DeleteAllAsync(GetBloodTransfusionsInput input);
}