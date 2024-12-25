using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Medicines;

public class MedicineManager(IMedicineRepository medicineRepository) : DomainService
{
    public virtual async Task<Medicine> CreateAsync(
        string name
    )
    {
        var entity = new Medicine(GuidGenerator.Create(), name);
        return await medicineRepository.InsertAsync(entity);
    }

    public virtual async Task<Medicine> UpdateAsync(
        Guid id,
        string name,
        string? concurrencyStamp = null
    )
    {
        var entity = await medicineRepository.GetAsync(id);
        entity.SetName(name);
        entity.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await medicineRepository.UpdateAsync(entity);
    }
}