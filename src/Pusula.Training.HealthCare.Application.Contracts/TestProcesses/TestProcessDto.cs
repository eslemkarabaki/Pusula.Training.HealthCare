using Volo.Abp.Application.Dtos;
using System;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class TestProcessDto : AuditedEntityDto<Guid>
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public Guid TestId { get; set; }
        public string TestName { get; set; } = null!;
        public string Result { get; set; } = null!;
        public DateTime ResultDate { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;
    }
}
