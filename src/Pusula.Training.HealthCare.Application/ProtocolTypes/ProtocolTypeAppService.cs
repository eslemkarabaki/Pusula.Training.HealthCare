using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.ProtocolTypes;

[RemoteService(IsEnabled = false)]
public class ProtocolTypeAppService(IProtocolTypeRepository protocolTypeRepository)
    : HealthCareAppService, IProtocolTypeAppService
{
    public async Task<IEnumerable<ProtocolTypeDto>> GetListAsync() =>
        ObjectMapper.Map<IEnumerable<ProtocolType>, IEnumerable<ProtocolTypeDto>>(
            await protocolTypeRepository.GetListAsync()
        );
}