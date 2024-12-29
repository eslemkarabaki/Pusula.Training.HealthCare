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


namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public class EfCoreRadiologyExaminationProcedureRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, RadiologyExaminationProcedure, Guid>(dbContextProvider), IRadiologyExaminationProcedureRepository
    {

        public virtual async Task DeleteAllAsync(string? filterText = null, string? result = null, DateTime? resultDate = null, Guid? doctorId = null, Guid? protocolId = null, Guid? RadiologyExaminationId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, result, resultDate, doctorId, protocolId, RadiologyExaminationId);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(string? filterText = null, string? result = null, DateTime? resultDate = null, Guid? doctorId = null, Guid? protocolId = null, Guid? RadiologyExaminationId = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, result, resultDate, doctorId, protocolId, RadiologyExaminationId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<RadiologyExaminationProcedure>> GetListAsync(string? filterText = null, string? result = null, DateTime? resultDate = null, Guid? doctorId = null, Guid? protocolId = null, Guid? RadiologyExaminationId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, result, resultDate, doctorId, protocolId, RadiologyExaminationId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RadiologyExaminationProcedureConsts.GetDefaultSorting(false) : sorting);

            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual IQueryable<RadiologyExaminationProcedure> ApplyFilter(
            IQueryable<RadiologyExaminationProcedure> query,
            string? filterText = null,
            string? result = null,
            DateTime? resultDate = null,
            Guid? doctorId = null,
            Guid? protocolId = null,
            Guid? RadiologyExaminationId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Result.Contains(filterText))
                .WhereIf(!string.IsNullOrWhiteSpace(result), e => e.Result.Contains(result))
                .WhereIf(resultDate != null, e => e.ResultDate == resultDate)
                .WhereIf(doctorId != null, e => e.DoctorId == doctorId)
                .WhereIf(protocolId != null, e => e.ProtocolId == protocolId)
                .WhereIf(RadiologyExaminationId != null, e => e.RadiologyExaminationId == RadiologyExaminationId);
        }
    }
}
