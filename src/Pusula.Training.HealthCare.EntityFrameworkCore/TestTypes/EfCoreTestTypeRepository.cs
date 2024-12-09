using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.TestTypes
{
    public class EfCoreTestTypeRepository : EfCoreRepository<HealthCareDbContext, TestType, Guid>, ITestTypeRepository
    {
        public EfCoreTestTypeRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<TestType>> GetListAsync(
            string? filterText = null,
            string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, name);

            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestTypeConsts.GetDefaultSorting(false) : sorting);

            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, name);
            return await query.LongCountAsync(cancellationToken);
        }

        private IQueryable<TestType> ApplyFilter(
            IQueryable<TestType> query,
            string? filterText,
            string? name)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), x => x.Name.Contains(filterText))
                .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name.Contains(name!));
        }

        public Task DeleteAllAsync(string? filterText, string? name)
        {
            throw new NotImplementedException();
        }
    }
}
