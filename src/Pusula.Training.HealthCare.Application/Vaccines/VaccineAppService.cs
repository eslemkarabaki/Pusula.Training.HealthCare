using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Vaccines;

[RemoteService(false)]
[Authorize(HealthCarePermissions.Vaccines.Default)]
public class VaccineAppService(IVaccineRepository vaccineRepository, VaccineManager vaccineManager)
    : HealthCareAppService, IVaccineAppService
{
    public async Task<VaccineDto> GetAsync(Guid id) =>
        ObjectMapper.Map<Vaccine, VaccineDto>(await vaccineRepository.GetAsync(id));

    public async Task<List<VaccineDto>> GetListAsync(GetVaccinesInput input) =>
        ObjectMapper.Map<List<Vaccine>, List<VaccineDto>>(
            await vaccineRepository.GetListAsync(input.Name)
        );

    [Authorize(HealthCarePermissions.Vaccines.Create)]
    public async Task<VaccineDto> CreateAsync(VaccineCreateDto input)
    {
        var vaccine = await vaccineManager.CreateAsync(input.Name);
        return ObjectMapper.Map<Vaccine, VaccineDto>(vaccine);
    }

    [Authorize(HealthCarePermissions.Vaccines.Edit)]
    public async Task<VaccineDto> UpdateAsync(Guid id, VaccineUpdateDto input)
    {
        var vaccine = await vaccineManager.UpdateAsync(id, input.Name);
        return ObjectMapper.Map<Vaccine, VaccineDto>(vaccine);
    }

    [Authorize(HealthCarePermissions.Vaccines.Delete)]
    public async Task DeleteAsync(Guid id) => await vaccineRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Vaccines.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await vaccineRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.Vaccines.Delete)]
    public async Task DeleteAllAsync(GetVaccinesInput input) => await vaccineRepository.DeleteAllAsync(input.Name);
}