using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public interface ITestProcessRepository : IRepository<TestProcess, Guid>
    {
        Task<List<TestProcess>> GetListAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            string? result = null,
            DateTime? resultDateStart = null,
            DateTime? resultDateEnd = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            string? result = null,
            DateTime? resultDateStart = null,
            DateTime? resultDateEnd = null,
            CancellationToken cancellationToken = default);
    }
}
