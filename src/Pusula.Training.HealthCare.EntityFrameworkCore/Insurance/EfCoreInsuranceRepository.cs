using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Insurances
{
    public class EfCoreInsuranceRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Insurance, Guid>(dbContextProvider), IInsuranceRepository
    {
        #region DeleteAllAsync
        public virtual async Task DeleteAllAsync(
            string? filterText = null, string? name = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, name);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));

        }
        #endregion

        #region GetListAsync
        public virtual async Task<List<Insurance>> GetListAsync(
            string? filterText = null, string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name)
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? InsuranceConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);

        }

        #endregion

        #region GetCountAsync
        public virtual async Task<long> GetCountAsync(string? filterText = null, string? name = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));

        }
        #endregion

        #region ApplyFilter
        protected virtual IQueryable<Insurance> ApplyFilter(
            IQueryable<Insurance> query,
        string? filterText = null,
        string? name = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!));

        }
        #endregion
    }
}
