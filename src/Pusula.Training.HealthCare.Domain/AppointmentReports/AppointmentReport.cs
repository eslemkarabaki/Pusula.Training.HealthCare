using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class AppointmentReport: FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual DateTime ReportDate { get; private set; } // Raporun oluşturulduğu tarih
        [CanBeNull]
        public virtual string? PriorityNotes { get; private set; } // Öncelikli Durumlar veya Acil Notlar
        [CanBeNull]
        public virtual string? DoctorNotes { get; private set; } // Doktorun kendi notları

        public virtual Guid AppointmentId { get; private set; }

        protected AppointmentReport() 
        {
            ReportDate = DateTime.Now;
            PriorityNotes = string.Empty;
            DoctorNotes = string.Empty;
        }

        public AppointmentReport(Guid id, Guid appointmentId,
            DateTime reportDate, string priorityNotes, string doctorNotes)
        {
            SetReportDate(reportDate);
            SetPriorityNotes(priorityNotes);
            SetDoctorNotes(doctorNotes);
            SetAppointmentId(appointmentId);

        }

        public void SetReportDate(DateTime reportDate) => ReportDate = Check.NotNull(reportDate, nameof(reportDate));
        public void SetPriorityNotes(string? priorityNotes) => PriorityNotes = Check.Length(priorityNotes, nameof(priorityNotes), AppointmentReportConsts.PriorityNotesMaxLength, AppointmentReportConsts.PriorityNotesMinLength);
        public void SetDoctorNotes(string? doctorNotes) => DoctorNotes = Check.Length(doctorNotes, nameof(doctorNotes), AppointmentReportConsts.DoctorNotesMaxLength, AppointmentReportConsts.DoctorNotesMinLength);
        public void SetAppointmentId(Guid appointmentId) => AppointmentId = Check.NotDefaultOrNull<Guid>(appointmentId, nameof(appointmentId));

    }
}
