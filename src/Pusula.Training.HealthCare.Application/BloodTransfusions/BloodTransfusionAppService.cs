using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.BloodTransfusions;

[RemoteService(false)]
[Authorize(HealthCarePermissions.BloodTransfusions.Default)]
public class BloodTransfusionAppService(
    IBloodTransfusionRepository bloodTransfusionRepository,
    BloodTransfusionManager bloodTransfusionManager
)
    : HealthCareAppService, IBloodTransfusionAppService
{
    public async Task<BloodTransfusionDto> GetAsync(Guid id) =>
        ObjectMapper.Map<BloodTransfusion, BloodTransfusionDto>(await bloodTransfusionRepository.GetAsync(id));

    public async Task<List<BloodTransfusionDto>> GetListAsync(GetBloodTransfusionsInput input) =>
        ObjectMapper.Map<List<BloodTransfusion>, List<BloodTransfusionDto>>(
            await bloodTransfusionRepository.GetListAsync(input.Name)
        );

    [Authorize(HealthCarePermissions.BloodTransfusions.Create)]
    public async Task<BloodTransfusionDto> CreateAsync(BloodTransfusionCreateDto input)
    {
        var bloodTransfusion = await bloodTransfusionManager.CreateAsync(input.Name);
        return ObjectMapper.Map<BloodTransfusion, BloodTransfusionDto>(bloodTransfusion);
    }

    [Authorize(HealthCarePermissions.BloodTransfusions.Edit)]
    public async Task<BloodTransfusionDto> UpdateAsync(Guid id, BloodTransfusionUpdateDto input)
    {
        var bloodTransfusion = await bloodTransfusionManager.UpdateAsync(id, input.Name);
        return ObjectMapper.Map<BloodTransfusion, BloodTransfusionDto>(bloodTransfusion);
    }

    [Authorize(HealthCarePermissions.BloodTransfusions.Delete)]
    public async Task DeleteAsync(Guid id) => await bloodTransfusionRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.BloodTransfusions.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await bloodTransfusionRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.BloodTransfusions.Delete)]
    public async Task DeleteAllAsync(GetBloodTransfusionsInput input) =>
        await bloodTransfusionRepository.DeleteAllAsync(input.Name);
}