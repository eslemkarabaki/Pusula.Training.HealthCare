using System;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentUpdateDto:IHasConcurrencyStamp
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public EnumStatus Status { get; set; }
        public string? Notes { get; set; }
        public Guid AppointmentTypeId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }

        public string? ConcurrencyStamp { get; set; }
    }
}
