using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Educations;

[RemoteService(false)]
[Authorize(HealthCarePermissions.Educations.Default)]
public class EducationAppService(IEducationRepository educationRepository, EducationManager educationManager)
    : HealthCareAppService, IEducationAppService
{
    public async Task<EducationDto> GetAsync(Guid id) =>
        ObjectMapper.Map<Education, EducationDto>(await educationRepository.GetAsync(id));

    public async Task<List<EducationDto>> GetListAsync(GetEducationsInput input) =>
        ObjectMapper.Map<List<Education>, List<EducationDto>>(
            await educationRepository.GetListAsync(input.Name)
        );

    [Authorize(HealthCarePermissions.Educations.Create)]
    public async Task<EducationDto> CreateAsync(EducationCreateDto input)
    {
        var education = await educationManager.CreateAsync(input.Name);
        return ObjectMapper.Map<Education, EducationDto>(education);
    }

    [Authorize(HealthCarePermissions.Educations.Edit)]
    public async Task<EducationDto> UpdateAsync(Guid id, EducationUpdateDto input)
    {
        var education = await educationManager.UpdateAsync(id, input.Name);
        return ObjectMapper.Map<Education, EducationDto>(education);
    }

    [Authorize(HealthCarePermissions.Educations.Delete)]
    public async Task DeleteAsync(Guid id) => await educationRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Educations.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await educationRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.Educations.Delete)]
    public async Task DeleteAllAsync(GetEducationsInput input) => await educationRepository.DeleteAllAsync(input.Name);
}