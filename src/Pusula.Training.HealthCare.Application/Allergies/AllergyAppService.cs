using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Allergies;

[RemoteService(false)]
[Authorize(HealthCarePermissions.Allergies.Default)]
public class AllergyAppService(IAllergyRepository allergyRepository, AllergyManager allergyManager)
    : HealthCareAppService, IAllergyAppService
{
    public async Task<AllergyDto> GetAsync(Guid id) =>
        ObjectMapper.Map<Allergy, AllergyDto>(await allergyRepository.GetAsync(id));

    public async Task<List<AllergyDto>> GetListAsync(GetAllergiesInput input) =>
        ObjectMapper.Map<List<Allergy>, List<AllergyDto>>(
            await allergyRepository.GetListAsync(input.Name)
        );

    [Authorize(HealthCarePermissions.Allergies.Create)]
    public async Task<AllergyDto> CreateAsync(AllergyCreateDto input)
    {
        var allergy = await allergyManager.CreateAsync(input.Name);
        return ObjectMapper.Map<Allergy, AllergyDto>(allergy);
    }

    [Authorize(HealthCarePermissions.Allergies.Edit)]
    public async Task<AllergyDto> UpdateAsync(Guid id, AllergyUpdateDto input)
    {
        var allergy = await allergyManager.UpdateAsync(id, input.Name);
        return ObjectMapper.Map<Allergy, AllergyDto>(allergy);
    }

    [Authorize(HealthCarePermissions.Allergies.Delete)]
    public async Task DeleteAsync(Guid id) => await allergyRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Allergies.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await allergyRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.Allergies.Delete)]
    public async Task DeleteAllAsync(GetAllergiesInput input) => await allergyRepository.DeleteAllAsync(input.Name);
}