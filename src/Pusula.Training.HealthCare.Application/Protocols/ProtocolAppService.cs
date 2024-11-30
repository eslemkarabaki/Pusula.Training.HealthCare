using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Protocols;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Protocols.Default)]
public class ProtocolAppService(IProtocolRepository protocolRepository, ProtocolManager protocolManager)
    : HealthCareAppService, IProtocolAppService
{
    [Authorize(HealthCarePermissions.Protocols.Delete)]
    public virtual async Task DeleteAsync(Guid id) => await protocolRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Protocols.Create)]
    public virtual async Task<ProtocolDto> CreateAsync(ProtocolCreateDto input)
    {
        var protocol = await protocolManager.CreateAsync(
            input.PatientId, input.DoctorId, input.DepartmentId, input.ProtocolTypeId, input.Description, input.Status
        );

        return ObjectMapper.Map<Protocol, ProtocolDto>(protocol);
    }

    [Authorize(HealthCarePermissions.Protocols.Edit)]
    public async Task<ProtocolDto> UpdateAsync(Guid id, ProtocolUpdateDto input)
    {
        var protocol = await protocolManager.UpdateAsync(id, input.Description, input.Status);
        return ObjectMapper.Map<Protocol, ProtocolDto>(protocol);
    }

    [Authorize(HealthCarePermissions.Protocols.Edit)]
    public virtual async Task<ProtocolDto> UpdateDescriptionAsync(Guid id, string? description)
    {
        var protocol = await protocolManager.UpdateDescriptionAsync(id, description);
        return ObjectMapper.Map<Protocol, ProtocolDto>(protocol);
    }

    [Authorize(HealthCarePermissions.Protocols.Edit)]
    public virtual async Task<ProtocolDto> UpdateStatusAsync(Guid id, EnumProtocolStatus status)
    {
        var protocol = await protocolManager.UpdateStatusAsync(id, status);
        return ObjectMapper.Map<Protocol, ProtocolDto>(protocol);
    }

    [Authorize(HealthCarePermissions.Protocols.Edit)]
    public virtual async Task<ProtocolDto> CompleteAsync(Guid id)
    {
        var protocol = await protocolManager.CompleteAsync(id);
        return ObjectMapper.Map<Protocol, ProtocolDto>(protocol);
    }

    [Authorize(HealthCarePermissions.Protocols.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> protocolIds) =>
        await protocolRepository.DeleteManyAsync(protocolIds);
}