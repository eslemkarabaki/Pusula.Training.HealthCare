using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Appointments
{

    public class Appointment : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual DateTime AppointmentStartDate { get;  set; }
        [NotNull]
        public virtual DateTime AppointmentEndDate { get;  set; }
        [NotNull]
        public virtual EnumStatus Status { get;  set; }

        [CanBeNull]
        public virtual string? Notes { get; set; }        

        public virtual Guid AppointmentTypeId { get; set; }
        public virtual Guid DepartmentId { get; set; }
        public virtual Guid DoctorId { get; set; }
        public virtual Guid PatientId { get; set; }

        protected Appointment()
        {
            AppointmentStartDate = DateTime.Now;
            AppointmentEndDate = DateTime.Now;
            Notes = string.Empty;
        }

        public Appointment(Guid id, Guid appointmentTypeId, 
            Guid departmentId, Guid doctorId, 
            Guid patientId, DateTime appointmentStartDate,
            DateTime appointmentEndDate, string notes,
            EnumStatus status = default)
        {
            Check.NotNullOrWhiteSpace(appointmentTypeId.ToString(), nameof(appointmentTypeId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(appointmentStartDate, nameof(appointmentStartDate));
            Check.NotNull(appointmentEndDate, nameof(appointmentEndDate));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.NotNullOrWhiteSpace(notes, nameof(notes));
                       

            Id = id;
            AppointmentTypeId = appointmentTypeId;
            DepartmentId = departmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            AppointmentStartDate = appointmentStartDate;
            AppointmentEndDate = appointmentEndDate;
            Notes = notes;
            Status = status;
           
        }

    }

}

