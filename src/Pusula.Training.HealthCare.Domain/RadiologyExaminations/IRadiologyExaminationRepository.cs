using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public interface IRadiologyExaminationRepository : IRepository<RadiologyExamination, Guid>
    {
        Task DeleteAllAsync(
            string? filterText = null,
            string? name = null,
            string? examinationCode = null,
            Guid? groupId = null,
            CancellationToken cancellationToken = default
        );

        Task<List<RadiologyExamination>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? examinationCode = null,
            Guid? groupId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            string? examinationCode = null,
            Guid? groupId = null,
            CancellationToken cancellationToken = default
        );

    }
}
