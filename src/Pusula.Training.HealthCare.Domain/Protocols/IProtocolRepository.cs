using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Protocols;

public interface IProtocolRepository : IRepository<Protocol, Guid>
{
    Task<Protocol> GetWithDetailsAsync(
        int protocolNo,
        CancellationToken cancellationToken = default
    );

    Task<List<Protocol>> GetListAsync(
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        Guid? protocolTypeActionId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<List<Protocol>> GetListWithDetailsAsync(
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        Guid? protocolTypeActionId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<List<Protocol>> GetDoctorWorkListWithDetailsAsync(
        Guid doctorId,
        ICollection<EnumProtocolStatus>? status = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        Guid? protocolTypeActionId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountForDoctorWorkListAsync(
        Guid doctorId,
        ICollection<EnumProtocolStatus>? status = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken cancellationToken = default
    );
}