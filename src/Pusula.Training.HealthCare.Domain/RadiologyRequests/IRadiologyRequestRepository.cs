using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.RadiologyRequests;
public interface IRadiologyRequestRepository : IRepository<RadiologyRequest, Guid>
{
    Task DeleteAllAsync(
        string? filterText = null,
        DateTime? requestDate = null,
        Guid? protocolId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        CancellationToken cancellationToken = default);

    Task<List<RadiologyRequest>> GetListAsync(
        string? filterText = null,
        DateTime? requestDate = null,
        Guid? protocolId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(
        string? filterText = null,
        DateTime? requestDate = null,
        Guid? protocolId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        CancellationToken cancellationToken = default);

    Task<RadiologyRequestWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default);
    Task<List<RadiologyRequestWithNavigationProperties>> GetListRadiologyRequestWithNavigationPropertiesAsync(
        string? filterText = null,
        DateTime? requestDate = null,
        Guid? protocolId = null,
        Guid? departmentId = null,
        Guid? doctorId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
}