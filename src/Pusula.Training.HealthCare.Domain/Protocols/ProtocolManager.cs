using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Protocols;

public class ProtocolManager(IProtocolRepository protocolRepository) : DomainService
{
    public virtual async Task<Protocol> CreateAsync(
        Guid patientId,
        Guid doctorId,
        Guid departmentId,
        Guid typeId,
        string? description,
        EnumProtocolStatus status
    )
    {
        var protocol = new Protocol(
            GuidGenerator.Create(), patientId, doctorId, departmentId, typeId, description, status, DateTime.Now
        );

        return await protocolRepository.InsertAsync(protocol);
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