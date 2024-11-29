using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class EfCoreRadiologyExaminationDocumentRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : EfCoreRepository<HealthCareDbContext, RadiologyExaminationDocument, Guid>(dbContextProvider),
            IRadiologyExaminationDocumentRepository
    {
        public virtual async Task DeleteAllAsync(string? filterText = null, string? documentName = null, string? documentPath = null, DateTime? uploadDate = null, Guid? RadiologyExaminationProcedureId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, documentName, documentPath, uploadDate, RadiologyExaminationProcedureId);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(string? filterText = null, string? documentName = null, string? documentPath = null, DateTime? uploadDate = null, Guid? RadiologyExaminationProcedureId = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, documentName, documentPath, uploadDate, RadiologyExaminationProcedureId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<RadiologyExaminationDocument>> GetListAsync(string? filterText = null, string? documentName = null, string? documentPath = null, DateTime? uploadDate = null, Guid? RadiologyExaminationProcedureId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, documentName, documentPath, uploadDate, RadiologyExaminationProcedureId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RadiologyExaminationDocumentConsts.GetDefaultSorting(false) : sorting);

            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<RadiologyExaminationDocument> ApplyFilter(
            IQueryable<RadiologyExaminationDocument> query,
            string? filterText = null,
            string? documentName = null,
            string? documentPath = null,
            DateTime? uploadDate = null,
            Guid? RadiologyExaminationProcedureId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.DocumentName.Contains(filterText))
                .WhereIf(!string.IsNullOrWhiteSpace(documentName), e => e.DocumentName.Contains(documentName))
                .WhereIf(!string.IsNullOrWhiteSpace(documentPath), e => e.DocumentPath.Contains(documentPath))
                .WhereIf(uploadDate != null, e => e.UploadDate == uploadDate)
                .WhereIf(RadiologyExaminationProcedureId != null, e => e.RadiologyExaminationProcedureId == RadiologyExaminationProcedureId);
        }
    }
}
