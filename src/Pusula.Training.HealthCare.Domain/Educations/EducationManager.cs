using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Educations;

public class EducationManager(IEducationRepository educationRepository) : DomainService
{
    public virtual async Task<Education> CreateAsync(
        string name
    )
    {
        var entity = new Education(GuidGenerator.Create(), name);
        return await educationRepository.InsertAsync(entity);
    }

    public virtual async Task<Education> UpdateAsync(
        Guid id,
        string name,
        string? concurrencyStamp = null
    )
    {
        var entity = await educationRepository.GetAsync(id);
        entity.SetName(name);
        entity.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await educationRepository.UpdateAsync(entity);
    }
}