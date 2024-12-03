using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public interface IRadiologyExaminationDocumentRepository : IRepository<RadiologyExaminationDocument, Guid>
    {
        Task DeleteAllAsync(
            string? filterText = null,
            string? documentName = null,
            string? documentPath = null,
            DateTime? uploadDate = null,
            Guid? RadiologyExaminationProcedureId = null,
            CancellationToken cancellationToken = default);
        Task<List<RadiologyExaminationDocument>> GetListAsync(
                    string? filterText = null,
                    string? documentName = null,
                    string? documentPath = null,
                    DateTime? uploadDate = null,
                    Guid? RadiologyExaminationProcedureId = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? documentName = null,
            string? documentPath = null,
            DateTime? uploadDate = null,
            Guid? RadiologyExaminationProcedureId = null,
            CancellationToken cancellationToken = default);
    }
}
