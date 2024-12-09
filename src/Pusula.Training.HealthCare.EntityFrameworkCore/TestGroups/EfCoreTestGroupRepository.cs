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

namespace Pusula.Training.HealthCare.TestGroups
{
    public class EfCoreTestGroupRepository : EfCoreRepository<HealthCareDbContext, TestGroup, Guid>, ITestGroupRepository
    {
        public EfCoreTestGroupRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyFilter(query, filterText, code, name);
            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<TestGroup>> GetListAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, code, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestGroupConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? code = null,
            string? name = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, code, name);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<TestGroup> ApplyFilter(
            IQueryable<TestGroup> query,
            string? filterText = null,
            string? code = null,
            string? name = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText!) || e.Name.Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code!))
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!));
        }

        public Task DeleteAllAsync(string? filterText, string? code, string? name)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllByFilterAsync(string? filterText, string? code, string? name)
        {
            throw new NotImplementedException();
        }
    }
}
