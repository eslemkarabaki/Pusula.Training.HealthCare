using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Educations;

public interface IEducationAppService : IApplicationService
{
    Task<EducationDto> GetAsync(Guid id);
    Task<List<EducationDto>> GetListAsync(GetEducationsInput input);
    Task<EducationDto> CreateAsync(EducationCreateDto input);

    Task<EducationDto> UpdateAsync(Guid id, EducationUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> ids);
    Task DeleteAllAsync(GetEducationsInput input);
}