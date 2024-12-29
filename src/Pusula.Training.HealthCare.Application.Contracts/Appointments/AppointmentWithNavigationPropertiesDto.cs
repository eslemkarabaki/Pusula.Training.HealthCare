using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentWithNavigationPropertiesDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public AppointmentDto Appointment { get; set; } = null!;
        public AppointmentTypeDto AppointmentType { get; set; } = null!;
        public DepartmentDto Department { get; set; } = null!;
        public DoctorDto Doctor { get; set; }= null!;
        public PatientDto Patient { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
