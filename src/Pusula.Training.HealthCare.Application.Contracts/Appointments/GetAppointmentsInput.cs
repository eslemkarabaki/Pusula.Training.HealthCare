using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Appointments
{
    public class GetAppointmentsInput:PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public DateTime? AppointmentStartDate { get; set; }
        public DateTime? AppointmentEndDate { get; set; }
        public EnumStatus? Status { get; set; }
        public string? Notes { get; set; }
        public Guid? AppointmentTypeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? PatientId { get; set; }

        public GetAppointmentsInput()
        {
        }
    }
}
