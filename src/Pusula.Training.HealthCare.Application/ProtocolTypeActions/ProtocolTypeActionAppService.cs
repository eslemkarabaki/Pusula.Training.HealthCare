using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.ProtocolTypes;
using Volo.Abp;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.ProtocolTypes.Default)]
public class ProtocolTypeActionAppService(
    IProtocolTypeActionRepository protocolTypeActionRepository,
    ProtocolTypeActionManager protocolTypeActionManager
)
    : HealthCareAppService, IProtocolTypeActionAppService
{
    public async Task<ProtocolTypeActionDto> GetAsync(Guid id) =>
        ObjectMapper.Map<ProtocolTypeAction, ProtocolTypeActionDto>(await protocolTypeActionRepository.GetAsync(id));

    public async Task<IEnumerable<ProtocolTypeActionDto>> GetAllAsync(Guid protocolTypeId) =>
        ObjectMapper.Map<IEnumerable<ProtocolTypeAction>, IEnumerable<ProtocolTypeActionDto>>(
            await protocolTypeActionRepository.GetListAsync(e => e.ProtocolTypeId == protocolTypeId)
        );

    [Authorize(HealthCarePermissions.ProtocolTypes.Create)]
    public async Task<ProtocolTypeActionDto> CreateAsync(ProtocolTypeActionCreateDto dto) =>
        ObjectMapper.Map<ProtocolTypeAction, ProtocolTypeActionDto>(
            await protocolTypeActionManager.CreateAsync(dto.Name, dto.ProtocolTypeId)
        );

    [Authorize(HealthCarePermissions.ProtocolTypes.Edit)]
    public async Task<ProtocolTypeActionDto> UpdateAsync(Guid id, ProtocolTypeActionUpdateDto dto) =>
        ObjectMapper.Map<ProtocolTypeAction, ProtocolTypeActionDto>(
            await protocolTypeActionManager.UpdateAsync(id, dto.Name)
        );
}