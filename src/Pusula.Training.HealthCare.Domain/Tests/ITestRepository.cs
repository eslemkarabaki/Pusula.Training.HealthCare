using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Tests
{
    public interface ITestRepository : IRepository<Test, Guid>
    {
        Task<List<Test>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            Guid? testGroupId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            Guid? testGroupId = null,
            CancellationToken cancellationToken = default);
        Task DeleteAllAsync(string? filterText, string? code, string? name, Guid? testGroupId);
    }
}
