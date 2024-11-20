using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentCreateDto
    {
        [Required]
        public DateTime AppointmentStartDate { get; set; }
        [Required]
        public DateTime AppointmentEndDate { get; set; }
        [Required]
        public EnumStatus Status { get; set; }
        [StringLength(AppointmentConsts.NotesMaxLength)]
        public string Notes { get; set; } = null!;
        public Guid AppointmentTypeId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
    }
}
