using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public interface IProtocolTypeAppService : IApplicationService
{
    public Task<IEnumerable<ProtocolTypeDto>> GetListAsync();
}