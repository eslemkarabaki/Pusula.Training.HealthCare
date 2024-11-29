    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Pusula.Training.HealthCare.Tests;
    using Pusula.Training.HealthCare.EntityFrameworkCore;
    using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore;

    namespace Pusula.Training.HealthCare.Tests
    {
        public class EfCoreTestRepository : EfCoreRepository<HealthCareDbContext, Test, Guid>,ITestRepository 
        {
            public EfCoreTestRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
                : base(dbContextProvider)
            {
            }

            public virtual async Task DeleteAllAsync(
                string? filterText = null,
                string? code = null,
                string? name = null,
                Guid? groupId = null,
                CancellationToken cancellationToken = default)
            {
                var query = await GetQueryableAsync();
                query = ApplyFilter(query, filterText, code, name, groupId);
                var ids = query.Select(x => x.Id);
                await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
            }

            public virtual async Task<List<Test>> GetListAsync(
                string? filterText = null,
                string? code = null,
                string? name = null,
                Guid? groupId = null,
                string? sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default)
            {
                var query = ApplyFilter(await GetQueryableAsync(), filterText, code, name, groupId);
                query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestConsts.GetDefaultSorting(false) : sorting);
                return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
            }

            public virtual async Task<long> GetCountAsync(
                string? filterText = null,
                string? code = null,
                string? name = null,
                Guid? groupId = null,
                CancellationToken cancellationToken = default)
            {
                var query = ApplyFilter(await GetQueryableAsync(), filterText, code, name, groupId);
                return await query.LongCountAsync(GetCancellationToken(cancellationToken));
            }

            protected virtual IQueryable<Test> ApplyFilter(
                IQueryable<Test> query,
                string? filterText = null,
                string? code = null,
                string? name = null,
                Guid? groupId = null)
            {
                return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Code.Contains(filterText!) || e.Name.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(code), e => e.Code.Contains(code!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
                    .WhereIf(groupId.HasValue, e => e.GroupId == groupId.Value);
            }

        public Task DeleteAllAsync(string? filterText, string? code, string? name, Guid? groupId)
        {
            throw new NotImplementedException();
        }
    }

    }
