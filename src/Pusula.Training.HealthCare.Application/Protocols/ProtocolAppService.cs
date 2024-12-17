using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Protocols;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Protocols.Default, Roles = HealthCareRoles.Doctor)]
public class ProtocolAppService(IProtocolRepository protocolRepository, ProtocolManager protocolManager)
    : HealthCareAppService, IProtocolAppService
{
    [Authorize(HealthCarePermissions.Protocols.Delete)]
    public virtual async Task DeleteAsync(Guid id) => await protocolRepository.DeleteAsync(id);

    public async Task<PagedResultDto<ProtocolDto>> GetListAsync(GetProtocolsInput input)
    {
        var items = await protocolRepository.GetListAsync(
            input.PatientId, input.DoctorId, input.DepartmentId, input.ProtocolTypeId, input.ProtocolTypeActionId,
            input.Status,
            input.StartTime, input.EndTime, input.Sorting, input.MaxResultCount, input.SkipCount
        );
        var count = await protocolRepository.GetCountAsync(
            input.PatientId, input.DoctorId, input.DepartmentId, input.ProtocolTypeId, input.ProtocolTypeActionId,
            input.Status,
            input.StartTime, input.EndTime
        );

        return new PagedResultDto<ProtocolDto>(
            count, ObjectMapper.Map<List<Protocol>, List<ProtocolDto>>(items)
        );
    }

    public async Task<ProtocolDto> GetWithDetailsAsync(int protocolNo) =>
        ObjectMapper.Map<Protocol, ProtocolDto>(await protocolRepository.GetWithDetailsAsync(protocolNo));

    public async Task<PagedResultDto<ProtocolDto>> GetListWithDetailsAsync(GetProtocolsInput input)
    {
        var items = await protocolRepository.GetListWithDetailsAsync(
            input.PatientId, input.DoctorId, input.DepartmentId, input.ProtocolTypeId, input.ProtocolTypeActionId,
            input.Status,
            input.StartTime, input.EndTime, input.Sorting, input.MaxResultCount, input.SkipCount
        );
        var count = await protocolRepository.GetCountAsync(
            input.PatientId, input.DoctorId, input.DepartmentId, input.ProtocolTypeId, input.ProtocolTypeActionId,
            input.Status,
            input.StartTime, input.EndTime
        );

        return new PagedResultDto<ProtocolDto>(
            count, ObjectMapper.Map<List<Protocol>, List<ProtocolDto>>(items)
        );
    }

    public async Task<PagedResultDto<ProtocolDto>> GetDoctorWorkListWithDetailsAsync(GetDoctorWorkListInput input)
    {
        var items = await protocolRepository.GetDoctorWorkListWithDetailsAsync(
            input.DoctorId, input.Status, input.StartTime, input.EndTime, input.Sorting, input.MaxResultCount,
            input.SkipCount
        );
        var count = await protocolRepository.GetCountForDoctorWorkListAsync(
            input.DoctorId, input.Status, input.StartTime, input.EndTime
        );

        return new PagedResultDto<ProtocolDto>(
            count, ObjectMapper.Map<List<Protocol>, List<ProtocolDto>>(items)
        );
    }

    [Authorize(HealthCarePermissions.Protocols.Create)]
    public virtual async Task<ProtocolDto> CreateAsync(ProtocolCreateDto input)
    {
        var protocol = await protocolManager.CreateAsync(
            input.PatientId, input.DoctorId, input.DepartmentId, input.ProtocolTypeId, input.ProtocolTypeActionId,
            input.Description, input.Status
        );

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
}