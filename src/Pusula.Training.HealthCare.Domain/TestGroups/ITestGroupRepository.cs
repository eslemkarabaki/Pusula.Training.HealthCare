using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestGroups
{
    public interface ITestGroupRepository : IRepository<TestGroup, Guid>
    {
        Task<List<TestGroup>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            CancellationToken cancellationToken = default);
        Task DeleteAllAsync(string? filterText, string? code, string? name);
        Task DeleteAllByFilterAsync(string? filterText, string? code, string? name);
    }
}
