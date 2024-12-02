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

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class EfCoreRadiologyExaminationRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider) 
        : EfCoreRepository<HealthCareDbContext,
        RadiologyExamination, Guid>(dbContextProvider),
        IRadiologyExaminationRepository
    {
        public virtual async Task DeleteAllAsync(string? filterText = null, string? name = null, string? examinationCode = null, Guid? groupId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyFilter(query, filterText, name, examinationCode, groupId);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(string? filterText = null, string? name = null, string? examinationCode = null, Guid? groupId = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, examinationCode, groupId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<RadiologyExamination>> GetListAsync(string? filterText = null, string? name = null, string? examinationCode = null, Guid? groupId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, examinationCode, groupId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RadiologyExaminationConsts.GetDefaultSorting(false) : sorting);

            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected IQueryable<RadiologyExamination> ApplyFilter(IQueryable<RadiologyExamination> query, string? filterText = null, string? name = null, string? examinationCode = null, Guid? groupId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText))
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                .WhereIf(!string.IsNullOrWhiteSpace(examinationCode), e => e.ExaminationCode.Contains(examinationCode))
                .WhereIf(groupId.HasValue, e => e.GroupId == groupId);
        }
    }
}
