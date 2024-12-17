using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.ExaminationsPhysical;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public class EfCoreExaminationPhysicalRepository : EfCoreRepository<HealthCareDbContext, ExaminationPhysical, Guid>, IExaminationPhysicalRepository
    {
        public EfCoreExaminationPhysicalRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public virtual async Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? physicalNote = null, Guid? userId = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), physicalNote, userId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<ExaminationPhysical>> GetListAsync(
            string physicalNote = null, Guid? userId = null, string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), physicalNote, userId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExaminationPhysicalConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual IQueryable<ExaminationPhysical> ApplyFilter(
            IQueryable<ExaminationPhysical> query,
            string? physicalNote = null,
            Guid? userId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(physicalNote), e => e.PhysicalNote.Contains(physicalNote!))
                .WhereIf(userId.HasValue, e => e.UserId == userId.Value);
        }
    }
}