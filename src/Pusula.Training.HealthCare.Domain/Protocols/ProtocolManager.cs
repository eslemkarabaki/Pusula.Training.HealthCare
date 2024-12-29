using JetBrains.Annotations;
using Pusula.Training.HealthCare.Examinations;
using System;
using System.Data;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolManager(IProtocolRepository protocolRepository, ExaminationManager examinationManager) : DomainService
{
    public virtual async Task<Protocol> CreateAsync(
        Guid patientId,
        Guid doctorId,
        Guid departmentId,
        Guid protocolTypeId,
        Guid protocolTypeActionId,
        string? description,
        EnumProtocolStatus status
    )
    {
        
        var protocol = await protocolRepository.InsertAsync(new Protocol(
            GuidGenerator.Create(), patientId, doctorId, departmentId, protocolTypeId, protocolTypeActionId,
            description, status, DateTime.Now
        ));
        
        await examinationManager.CreateAsync(protocol.Id, doctorId, patientId, string.Empty, protocol.StartTime);
        return protocol;
       
    }

    public virtual async Task<Protocol> UpdateAsync(
        Guid id,
        string? description,
        EnumProtocolStatus status,
        string? concurrencyStamp = null
    )
    {
        var protocol = await protocolRepository.GetAsync(id);
        protocol.SetDescription(description);
        protocol.SetStatus(status);
        protocol.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await protocolRepository.UpdateAsync(protocol);
    }

    public virtual async Task<Protocol> UpdateDescriptionAsync(
        Guid id,
        string? description,
        string? concurrencyStamp = null
    )
    {
        var protocol = await protocolRepository.GetAsync(id);
        protocol.SetDescription(description);
        protocol.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await protocolRepository.UpdateAsync(protocol);
    }

    public virtual async Task<Protocol> UpdateStatusAsync(
        Guid id,
        EnumProtocolStatus status,
        string? concurrencyStamp = null
    )
    {
        var protocol = await protocolRepository.GetAsync(id);
        protocol.SetStatus(status);
        protocol.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await protocolRepository.UpdateAsync(protocol);
    }

    public virtual async Task<Protocol> CompleteAsync(Guid id, string? concurrencyStamp = null)
    {
        var protocol = await protocolRepository.GetAsync(id);
        protocol.SetStatus(EnumProtocolStatus.Completed);
        protocol.SetEndTime(DateTime.Now);
        protocol.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await protocolRepository.UpdateAsync(protocol);
    }
}