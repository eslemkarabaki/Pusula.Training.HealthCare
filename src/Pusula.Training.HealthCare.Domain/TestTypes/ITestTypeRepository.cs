using Pusula.Training.HealthCare.TestTypes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestTypes
{
    public interface ITestTypeRepository : IRepository<TestType, Guid>
    {
        Task<List<TestType>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            CancellationToken cancellationToken = default);
        Task DeleteAllAsync(string? filterText, string? name);
    }
}
