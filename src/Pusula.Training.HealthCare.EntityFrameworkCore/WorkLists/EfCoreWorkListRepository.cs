using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Laboratory;
using Pusula.Training.HealthCare.WorkLists;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace Pusula.Training.HealthCare.WorkLists
{
    public class EfCoreWorkListRepository : EfCoreRepository<HealthCareDbContext, WorkList, Guid>, IWorkListRepository
    {
        public EfCoreWorkListRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task DeleteAllAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            DateTime? scheduledDateStart = null,
            DateTime? scheduledDateEnd = null,
            bool? isCompleted = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            query = ApplyFilter(query, patientId, doctorId, testId, scheduledDateStart, scheduledDateEnd, isCompleted);
            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<WorkList>> GetListAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            DateTime? scheduledDateStart = null,
            DateTime? scheduledDateEnd = null,
            bool? isCompleted = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), patientId, doctorId, testId, scheduledDateStart, scheduledDateEnd, isCompleted);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WorkListConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            DateTime? scheduledDateStart = null,
            DateTime? scheduledDateEnd = null,
            bool? isCompleted = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), patientId, doctorId, testId, scheduledDateStart, scheduledDateEnd, isCompleted);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WorkList> ApplyFilter(
            IQueryable<WorkList> query,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? testId = null,
            DateTime? scheduledDateStart = null,
            DateTime? scheduledDateEnd = null,
            bool? isCompleted = null)
        {
            return query
                .WhereIf(patientId.HasValue, e => e.PatientId == patientId.Value)
                .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId.Value)
                .WhereIf(testId.HasValue, e => e.TestId == testId.Value)
                .WhereIf(scheduledDateStart.HasValue, e => e.ScheduledDate >= scheduledDateStart.Value)
                .WhereIf(scheduledDateEnd.HasValue, e => e.ScheduledDate <= scheduledDateEnd.Value)
                .WhereIf(isCompleted.HasValue, e => e.IsCompleted == isCompleted.Value);
        }

        public virtual async Task DeleteAllAsync(string? filterText, string? code, string? name, Guid? departmentId)
        {
            var query = await GetQueryableAsync();


            query = ApplyFilter(query, filterText, code, name, departmentId);


            var ids = query.Select(x => x.Id);

            await DeleteManyAsync(ids);
        }

        public virtual async Task<List<WorkList>> GetListAsync(string? filterText, string? code, string? name, Guid? departmentId, string? sorting, int maxResultCount, int skipCount)
        {
            var query = await GetQueryableAsync();


            query = ApplyFilter(query, filterText, code, name, departmentId);


            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "ScheduledDate DESC" : sorting);

            return await query.PageBy(skipCount, maxResultCount).ToListAsync();
        }


        public virtual async Task<long> GetCountAsync(string? filterText, string? code, string? name, Guid? departmentId)
        {
            var query = await GetQueryableAsync();


            query = ApplyFilter(query, filterText, code, name, departmentId);


            return await query.LongCountAsync();
        }


        public virtual async Task<List<WorkList>> GetListAsync(string? filterText, string? code, string? name, Guid? departmentId)
        {
            var query = await GetQueryableAsync();


            query = ApplyFilter(query, filterText, code, name, departmentId);


            return await query.ToListAsync();
        }


        protected virtual IQueryable<WorkList> ApplyFilter(
            IQueryable<WorkList> query,
            string? filterText = null,
            string? code = null,
            string? name = null,
            Guid? departmentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), x => x.PatientId.ToString().Contains(filterText!) || x.DoctorId.ToString().Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(code), x => x.TestId.ToString().Contains(code!))
                .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.TestId.ToString().Contains(name!))
                .WhereIf(departmentId.HasValue, x => x.DoctorId == departmentId.Value);
        }
    }
}
