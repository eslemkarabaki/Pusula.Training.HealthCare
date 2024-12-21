using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.BloodTransfusions;

public class BloodTransfusionManager(IBloodTransfusionRepository bloodTransfusionRepository) : DomainService
{
    public virtual async Task<BloodTransfusion> CreateAsync(
        string name
    )
    {
        var entity = new BloodTransfusion(GuidGenerator.Create(), name);
        return await bloodTransfusionRepository.InsertAsync(entity);
    }

    public virtual async Task<BloodTransfusion> UpdateAsync(
        Guid id,
        string name,
        string? concurrencyStamp = null
    )
    {
        var entity = await bloodTransfusionRepository.GetAsync(id);
        entity.SetName(name);
        entity.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await bloodTransfusionRepository.UpdateAsync(entity);
    }
}