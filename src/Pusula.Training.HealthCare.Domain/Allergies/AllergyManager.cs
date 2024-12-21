using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Allergies;

public class AllergyManager(IAllergyRepository allergyRepository) : DomainService
{
    public virtual async Task<Allergy> CreateAsync(
        string name
    )
    {
        var entity = new Allergy(GuidGenerator.Create(), name);
        return await allergyRepository.InsertAsync(entity);
    }

    public virtual async Task<Allergy> UpdateAsync(
        Guid id,
        string name,
        string? concurrencyStamp = null
    )
    {
        var entity = await allergyRepository.GetAsync(id);
        entity.SetName(name);
        entity.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await allergyRepository.UpdateAsync(entity);
    }
}