using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public class ProtocolTypeActionManager(IProtocolTypeActionRepository protocolTypeActionRepository) : DomainService
{
    public async Task<ProtocolTypeAction> CreateAsync(string name, Guid protocolTypeId) =>
        await protocolTypeActionRepository.InsertAsync(
            new ProtocolTypeAction(GuidGenerator.Create(), name, protocolTypeId)
        );

    public async Task<ProtocolTypeAction> UpdateAsync(
        Guid id,
        string name,
        string? concurrencyStamp = default
    )
    {
        var protocolTypeAction = await protocolTypeActionRepository.GetAsync(id);
        protocolTypeAction.SetName(name);
        protocolTypeAction.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await protocolTypeActionRepository.UpdateAsync(protocolTypeAction);
    }
}