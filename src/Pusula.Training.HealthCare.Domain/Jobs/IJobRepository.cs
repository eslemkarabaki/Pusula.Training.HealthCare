using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Jobs;

public interface IJobRepository : IRepository<Job, Guid>
{
    Task<List<Job>> GetListAsync(
        string? name = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task DeleteAllAsync(
        string? name = null,
        CancellationToken cancellationToken = default
    );
}