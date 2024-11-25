using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorDto : FullAuditedEntityDto<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public string WorkingHours { get; set; } = null!;
        public Guid TitleId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid HospitalId { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}
