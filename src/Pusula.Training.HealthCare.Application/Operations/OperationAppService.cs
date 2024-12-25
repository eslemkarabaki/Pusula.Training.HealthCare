using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Operations;

[RemoteService(false)]
[Authorize(HealthCarePermissions.Operations.Default)]
public class OperationAppService(IOperationRepository operationRepository, OperationManager operationManager)
    : HealthCareAppService, IOperationAppService
{
    public async Task<OperationDto> GetAsync(Guid id) =>
        ObjectMapper.Map<Operation, OperationDto>(await operationRepository.GetAsync(id));

    public async Task<List<OperationDto>> GetListAsync(GetOperationsInput input) =>
        ObjectMapper.Map<List<Operation>, List<OperationDto>>(
            await operationRepository.GetListAsync(input.Name)
        );

    [Authorize(HealthCarePermissions.Operations.Create)]
    public async Task<OperationDto> CreateAsync(OperationCreateDto input)
    {
        var operation = await operationManager.CreateAsync(input.Name);
        return ObjectMapper.Map<Operation, OperationDto>(operation);
    }

    [Authorize(HealthCarePermissions.Operations.Edit)]
    public async Task<OperationDto> UpdateAsync(Guid id, OperationUpdateDto input)
    {
        var operation = await operationManager.UpdateAsync(id, input.Name);
        return ObjectMapper.Map<Operation, OperationDto>(operation);
    }

    [Authorize(HealthCarePermissions.Operations.Delete)]
    public async Task DeleteAsync(Guid id) => await operationRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Operations.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> ids) => await operationRepository.DeleteManyAsync(ids);

    [Authorize(HealthCarePermissions.Operations.Delete)]
    public async Task DeleteAllAsync(GetOperationsInput input) => await operationRepository.DeleteAllAsync(input.Name);
}