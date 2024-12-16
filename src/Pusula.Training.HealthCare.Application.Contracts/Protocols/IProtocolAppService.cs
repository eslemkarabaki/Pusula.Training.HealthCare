using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Protocols;

public interface IProtocolAppService : IApplicationService
{
    Task<PagedResultDto<ProtocolDto>> GetListWithDetailsAsync(GetProtocolsInput input);
    Task<PagedResultDto<ProtocolDto>> GetDoctorWorkListWithDetailsAsync(GetDoctorWorkListInput input);
    
    Task<ProtocolDto> CreateAsync(ProtocolCreateDto input);

    Task<ProtocolDto> UpdateDescriptionAsync(Guid id, string description);
    Task<ProtocolDto> UpdateStatusAsync(Guid id, EnumProtocolStatus status);
    Task<ProtocolDto> CompleteAsync(Guid id);
}