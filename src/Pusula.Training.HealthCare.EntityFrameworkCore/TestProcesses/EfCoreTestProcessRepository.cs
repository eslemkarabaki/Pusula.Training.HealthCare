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

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class EfCoreTestProcessRepository : EfCoreRepository<HealthCareDbContext, TestProcess, Guid>, ITestProcessRepository
    {
        public EfCoreTestProcessRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<TestProcess>> GetListAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            string? result = null,
            DateTime? resultDateStart = null,
            DateTime? resultDateEnd = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), patientId, doctorId, testId, result, resultDateStart, resultDateEnd);

            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestProcessConsts.GetDefaultSorting(false) : sorting);

            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            string? result = null,
            DateTime? resultDateStart = null,
            DateTime? resultDateEnd = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), patientId, doctorId, testId, result, resultDateStart, resultDateEnd);
            return await query.LongCountAsync(cancellationToken);
        }

        private IQueryable<TestProcess> ApplyFilter(
            IQueryable<TestProcess> query,
            Guid? patientId,
            Guid? doctorId,
            Guid? testId,
            string? result,
            DateTime? resultDateStart,
            DateTime? resultDateEnd)
        {
            return query
                .WhereIf(patientId.HasValue, x => x.PatientId == patientId.Value)
                .WhereIf(doctorId.HasValue, x => x.DoctorId == doctorId.Value)
                .WhereIf(testId.HasValue, x => x.TestId == testId.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(result), x => x.Result.Contains(result!))
                .WhereIf(resultDateStart.HasValue, x => x.ResultDate >= resultDateStart.Value)
                .WhereIf(resultDateEnd.HasValue, x => x.ResultDate <= resultDateEnd.Value);
        }
    }
}
    