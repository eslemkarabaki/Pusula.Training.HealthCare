using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Vaccines;

public class VaccineManager(IVaccineRepository vaccineRepository) : DomainService
{
    public virtual async Task<Vaccine> CreateAsync(
        string name
    )
    {
        var entity = new Vaccine(GuidGenerator.Create(), name);
        return await vaccineRepository.InsertAsync(entity);
    }

    public virtual async Task<Vaccine> UpdateAsync(
        Guid id,
        string name,
        string? concurrencyStamp = null
    )
    {
        var entity = await vaccineRepository.GetAsync(id);
        entity.SetName(name);
        entity.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await vaccineRepository.UpdateAsync(entity);
    }
}