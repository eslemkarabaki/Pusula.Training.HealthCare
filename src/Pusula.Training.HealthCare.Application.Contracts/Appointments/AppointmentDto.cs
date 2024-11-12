using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime? AppointmentDate { get; set; }
        public EnumStatus? Status { get; set; }
        public string? Notes { get; set; } = null!;
        public Guid? HospitalId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? PatientId { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;

    }
}
