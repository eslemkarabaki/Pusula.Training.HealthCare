using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public class AppointmentReportDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime? ReportDate { get; set; }
        public string? PriorityNotes { get; set; } = null!;
        public string? DoctorNotes { get; set; } = null!;
        public Guid? AppointmentId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
