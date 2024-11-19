using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class TestProcessUpdateDto
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public Guid TestId { get; set; }

        [Required]
        [StringLength(500)]
        public string Result { get; set; } = null!;

        [Required]
        public DateTime ResultDate { get; set; }

        public string ConcurrencyStamp { get; set; } = null!;
    }
}
