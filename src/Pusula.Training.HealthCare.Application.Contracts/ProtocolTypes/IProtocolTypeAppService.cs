using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
public interface IProtocolTypeAppService : IApplicationService
{
        // Listeleme (sayfalama destekli)
        Task<PagedResultDto<ProtocolTypeDto>> GetListAsync(GetProtocolTypeInput input);
        Task<List<ProtocolTypeDto>> GetListAsync();

        // ID ile alma
        Task<ProtocolTypeDto> GetAsync(Guid id);

        // Oluşturma
        Task<ProtocolTypeDto> CreateAsync(ProtocolTypeCreateDto input);

        // Güncelleme
        Task<ProtocolTypeDto> UpdateAsync(Guid id, ProtocolTypeUpdateDto input);

        // Silme
        Task DeleteAsync(Guid id);
    }
}