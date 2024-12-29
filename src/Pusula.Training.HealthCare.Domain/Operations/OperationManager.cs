using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Operations;

public class OperationManager(IOperationRepository operationRepository) : DomainService
{
    public virtual async Task<Operation> CreateAsync(
        string name
    )
    {
        var entity = new Operation(GuidGenerator.Create(), name);
        return await operationRepository.InsertAsync(entity);
    }

    public virtual async Task<Operation> UpdateAsync(
        Guid id,
        string name,
        string? concurrencyStamp = null
    )
    {
        var entity = await operationRepository.GetAsync(id);
        entity.SetName(name);
        entity.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await operationRepository.UpdateAsync(entity);
    }
}