using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
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

            var appointmentReport = await appointmentReportRepository.GetAsync(id);
            appointmentReport.SetAppointmentId(appointmentId);
            appointmentReport.SetReportDate(reportDate);
            appointmentReport.SetPriorityNotes(priorityNotes);
            appointmentReport.SetDoctorNotes(doctorNotes);

            appointmentReport.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentReportRepository.UpdateAsync(appointmentReport);
        }
        #endregion
    }
}
