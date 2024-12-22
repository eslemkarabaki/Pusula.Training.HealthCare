using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Operations;

public interface IOperationAppService : IApplicationService
{
    Task<OperationDto> GetAsync(Guid id);
    Task<List<OperationDto>> GetListAsync(GetOperationsInput input);
    Task<OperationDto> CreateAsync(OperationCreateDto input);

    Task<OperationDto> UpdateAsync(Guid id, OperationUpdateDto input);

    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> ids);
    Task DeleteAllAsync(GetOperationsInput input);
}