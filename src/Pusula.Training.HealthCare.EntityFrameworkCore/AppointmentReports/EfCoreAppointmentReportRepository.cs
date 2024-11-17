using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class EfCoreAppointmentReportRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : EfCoreRepository<HealthCareDbContext, AppointmentReport, Guid>(dbContextProvider), IAppointmentReportRepository
    {
        #region DeleteAll
        public virtual async Task DeleteAllAsync(
            string? filterText = null,
            DateTime? reportDate = null, string? priorityNotes = null,
            string? doctorNotes = null, Guid? appointmentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, reportDate, priorityNotes, doctorNotes, appointmentId);

            var ids = query.Select(x => x.AppointmentReport.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));

        }
        #endregion

        #region GetWithNavigationProperties
        public virtual async Task<AppointmentReportWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(appointmentReport => new AppointmentReportWithNavigationProperties
                {
                    AppointmentReport = appointmentReport,
                    Appointment = dbContext.Set<Appointment>().FirstOrDefault(c => c.Id == appointmentReport.AppointmentId)!,

                }).FirstOrDefault()!;
        }
        #endregion

        #region GetListWithNavigationProperties
        public virtual async Task<List<AppointmentReportWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null, DateTime? reportDate = null,
            string? priorityNotes = null, string? doctorNotes = null,
            Guid? appointmentId = null, string? sorting = null,
            int maxResultCount = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, reportDate, priorityNotes, doctorNotes, appointmentId)
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentReportConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetQueryForNavigationProperties
        protected virtual async Task<IQueryable<AppointmentReportWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from appointmentReport in (await GetDbSetAsync())
                   join appointment in (await GetDbContextAsync()).Set<Appointment>() on appointmentReport.AppointmentId equals appointment.Id into appointments
                   from appointment in appointments.DefaultIfEmpty()
                   
                   select new AppointmentReportWithNavigationProperties
                   {
                       AppointmentReport = appointmentReport,
                       Appointment = appointment,                       
                   };
        }
        #endregion

        #region ApplyFiterWithNavigationProperties
        protected virtual IQueryable<AppointmentReportWithNavigationProperties> ApplyFilter(
            IQueryable<AppointmentReportWithNavigationProperties> query,
            string? filterText = null,
            DateTime? reportDate = null, string? priorityNotes = null,
            string? doctorNotes = null, Guid? appointmentId = null)
        {
            return query
                    .WhereIf(reportDate.HasValue, e => e.AppointmentReport.ReportDate >= reportDate!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(priorityNotes), e => e.AppointmentReport.PriorityNotes!.Contains(priorityNotes!))
                    .WhereIf(!string.IsNullOrWhiteSpace(doctorNotes), e => e.AppointmentReport.DoctorNotes!.Contains(doctorNotes!))
                    .WhereIf(appointmentId != null && appointmentId != Guid.Empty, e => e.Appointment != null && e.Appointment.Id == appointmentId);
        }
        #endregion

        #region GetList
        public virtual async Task<List<AppointmentReport>> GetListAsync(
             string? filterText = null, DateTime? reportDate = null,
            string? priorityNotes = null, string? doctorNotes = null,
            Guid? appointmentId = null, string? sorting = null,
            int maxResultCount = int.MaxValue, int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter(await GetQueryableAsync(), filterText, reportDate, priorityNotes, doctorNotes)
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentReportConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }
        #endregion

        #region GetCount
        public virtual async Task<long> GetCountAsync(
            string? filterText = null, DateTime? reportDate = null,
            string? priorityNotes = null, string? doctorNotes = null,
            Guid? appointmentId = null, CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, reportDate, priorityNotes, doctorNotes, appointmentId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }
        #endregion

        #region ApplyFilter
        protected virtual IQueryable<AppointmentReport> ApplyFilter(
        IQueryable<AppointmentReport> query,
        string? filterText = null, DateTime? reportDate = null, string? priorityNotes = null, string? doctorNotes = null)
        {
            return query
                .WhereIf(reportDate.HasValue, e => e.ReportDate >= reportDate!.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(priorityNotes), e => e.PriorityNotes!.Contains(priorityNotes!))
                .WhereIf(!string.IsNullOrWhiteSpace(doctorNotes), e => e.DoctorNotes!.Contains(doctorNotes!));
        }
        #endregion
    }
}
