using Pusula.Training.HealthCare.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentCreateDto
    {
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public EnumStatus Status { get; set; } = EnumStatus.Scheduled;
        [StringLength(AppointmentConsts.NoteMaxLength, MinimumLength =3)]
        public string Notes { get; set; } = null!;
        [Required]
        [NotEmptyGuid]
        public Guid AppointmentTypeId { get; set; }
        [Required]
        [NotEmptyGuid]

        public Guid DepartmentId { get; set; }
        [Required]
        [NotEmptyGuid]

        public Guid DoctorId { get; set; }
        [Required]
        [NotEmptyGuid]

        public Guid PatientId { get; set; }
    }
}
