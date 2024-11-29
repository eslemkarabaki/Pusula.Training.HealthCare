using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class AppointmentReport: FullAuditedAggregateRoot<Guid>
    {
        [JetBrains.Annotations.NotNull]
        public virtual DateTime ReportDate { get; set; } // Raporun oluşturulduğu tarih
        [CanBeNull]
        public virtual string? PriorityNotes { get; set; } // Öncelikli Durumlar veya Acil Notlar
        [CanBeNull]
        public virtual string? DoctorNotes { get; set; } // Doktorun kendi notları

        public virtual Guid AppointmentId { get; set; }

        protected AppointmentReport() 
        {
            ReportDate = DateTime.Now;
            PriorityNotes = string.Empty;
            DoctorNotes = string.Empty;
        }

        public AppointmentReport(Guid id, Guid appointmentId,
            DateTime reportDate, string priorityNotes, string doctorNotes)
        {
            Check.NotNullOrWhiteSpace(appointmentId.ToString(), nameof(appointmentId));
            Check.NotNull(reportDate, nameof(reportDate));
            Check.NotNullOrWhiteSpace(priorityNotes, nameof(priorityNotes));
            Check.NotNullOrWhiteSpace(doctorNotes, nameof(doctorNotes));

            Id = id;
            AppointmentId = appointmentId;
            ReportDate = reportDate;
            PriorityNotes = priorityNotes;
            DoctorNotes = doctorNotes;

        }

    }
}
