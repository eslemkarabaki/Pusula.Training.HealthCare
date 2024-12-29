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
        public virtual async Task DeleteAllAsync(string? filterText = null,  string? path = null, DateTime? uploadDate = null, Guid? itemId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText,  path, uploadDate, itemId);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(string? filterText = null,  string? path = null, DateTime? uploadDate = null, Guid? itemId = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText,  path, uploadDate, itemId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<RadiologyExaminationDocument>> GetListAsync(string? filterText = null,  string? path = null, DateTime? uploadDate = null, Guid? itemId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText,  path, uploadDate, itemId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RadiologyExaminationDocumentConsts.GetDefaultSorting(false) : sorting);

            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<RadiologyExaminationDocument> ApplyFilter(
            IQueryable<RadiologyExaminationDocument> query,
            string? filterText = null,
            
            string? path = null,
            DateTime? uploadDate = null,
            Guid? itemId = null)
        {
            return query 
                .WhereIf(!string.IsNullOrWhiteSpace(path), e => e.Path.Contains(path))
                .WhereIf(uploadDate != null, e => e.UploadDate == uploadDate)
                .WhereIf(itemId != null, e => e.ItemId == itemId);
        }
    }
}
