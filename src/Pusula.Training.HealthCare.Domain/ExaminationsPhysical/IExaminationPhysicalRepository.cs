using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public interface IExaminationPhysicalRepository : IRepository<ExaminationPhysical, Guid>
    {

        Task DeleteAllAsync(CancellationToken cancellationToken = default);

        Task<List<ExaminationPhysical>> GetListAsync(
            string? physicalNote = null,
            Guid? userId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default);

        // Retrieves the count of ExaminationPhysical records based on the filter criteria
        Task<long> GetCountAsync(
            string? physicalNote = null,
            Guid? userId = null,
            CancellationToken cancellationToken = default);

        // Retrieves a single ExaminationPhysical record by its ExaminationId
    }
}
