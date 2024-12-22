using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Allergies;

public interface IAllergyAppService : IApplicationService
{
    Task<AllergyDto> GetAsync(Guid id);
    Task<List<AllergyDto>> GetListAsync(GetAllergiesInput input);
    Task<AllergyDto> CreateAsync(AllergyCreateDto input);

    Task<AllergyDto> UpdateAsync(Guid id, AllergyUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> ids);
    Task DeleteAllAsync(GetAllergiesInput input);
}