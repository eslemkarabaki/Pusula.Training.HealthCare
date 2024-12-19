using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Examinations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Pusula.Training.HealthCare.Examinations
{
    public class EfCoreExaminationAnamnezRepository : EfCoreRepository<HealthCareDbContext, ExaminationAnamnez, Guid>, IExaminationAnamnezRepository
    {
        public EfCoreExaminationAnamnezRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Delete all records
        public virtual async Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        // Get count of records with optional filters
        public virtual async Task<long> GetCountAsync(
            string? identityNumber = null, Guid? examinationId = null, DateTime? startDate = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), identityNumber, examinationId, startDate);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        // Get list of records with optional filters, sorting, pagination
        public virtual async Task<List<ExaminationAnamnez>> GetListAsync(
            string? identityNumber = null, Guid? examinationId = null, DateTime? startDate = null, string sorting = null,
            int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), identityNumber, examinationId, startDate);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExaminationAnamnezConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        // Helper method to apply filters to the query
        protected virtual IQueryable<ExaminationAnamnez> ApplyFilter(
            IQueryable<ExaminationAnamnez> query,
            string? identityNumber = null,
            Guid? examinationId = null,
            DateTime? startDate = null)
        {
            return query
                .WhereIf(examinationId.HasValue, e => e.ExaminationId == examinationId.Value)
                .WhereIf(startDate.HasValue, e => e.StartDate.Date == startDate.Value.Date);
        }
    }
}
