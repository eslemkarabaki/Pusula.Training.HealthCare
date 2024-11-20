using JetBrains.Annotations;
using Pusula.Training.HealthCare.AppointmentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class AppointmentReportManager(IAppointmentReportRepository appointmentReportRepository):DomainService
    {
        #region CreateAsync
        public virtual async Task<AppointmentReport> CreateAsync(
            Guid appointmentId, DateTime reportDate,
            string? priorityNotes=null, string? doctorNotes=null)
        {
            Check.NotNullOrWhiteSpace(appointmentId.ToString(), nameof(appointmentId));
            Check.NotNull(reportDate, nameof(reportDate));
            Check.Length(priorityNotes, nameof(priorityNotes), AppointmentReportConsts.PriorityNotesMaxLength, AppointmentReportConsts.PriorityNotesMinLength);
            Check.Length(doctorNotes, nameof(doctorNotes), AppointmentReportConsts.DoctorNotesMaxLength, AppointmentReportConsts.DoctorNotesMinLength);

            var appointmentReport = new AppointmentReport(
                GuidGenerator.Create(),
                appointmentId, reportDate, priorityNotes!, doctorNotes!);

            return await appointmentReportRepository.InsertAsync(appointmentReport);
        }
        #endregion

        #region UpdateAsync 
        public virtual async Task<AppointmentReport> UpdateAsync(
            Guid id, Guid appointmentId, DateTime reportDate,
            string? priorityNotes = null, string? doctorNotes = null,
            [CanBeNull] string? concurrencyStamp=null)
        {
            Check.NotNullOrWhiteSpace(appointmentId.ToString(), nameof(appointmentId));
            Check.NotNull(reportDate, nameof(reportDate));
            Check.Length(priorityNotes, nameof(priorityNotes), AppointmentReportConsts.PriorityNotesMaxLength, AppointmentReportConsts.PriorityNotesMinLength);
            Check.Length(doctorNotes, nameof(doctorNotes), AppointmentReportConsts.DoctorNotesMaxLength, AppointmentReportConsts.DoctorNotesMinLength);

            var appointmentReport = await appointmentReportRepository.GetAsync(id);

            appointmentReport.AppointmentId = appointmentId;
            appointmentReport.ReportDate = reportDate;
            appointmentReport.PriorityNotes = priorityNotes;
            appointmentReport.DoctorNotes = doctorNotes;

            appointmentReport.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentReportRepository.UpdateAsync(appointmentReport);
        }
        #endregion
    }
}
