using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public interface IProtocolTypeActionAppService : IApplicationService
{
    Task<ProtocolTypeActionDto> GetAsync(Guid id);
    Task<IEnumerable<ProtocolTypeActionDto>> GetAllAsync(Guid protocolTypeId);
    Task<ProtocolTypeActionDto> CreateAsync(ProtocolTypeActionCreateDto dto);
    Task<ProtocolTypeActionDto> UpdateAsync(Guid id, ProtocolTypeActionUpdateDto dto);
}