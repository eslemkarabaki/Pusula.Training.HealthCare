using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public EnumStatus Status { get; set; } = EnumStatus.Scheduled;
        public string? Notes { get; set; } = null!;
        public Guid AppointmentTypeId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}
