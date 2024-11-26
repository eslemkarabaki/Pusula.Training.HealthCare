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
        public virtual DateTime StartTime { get;  set; }
        [NotNull]
        public virtual DateTime EndTime { get;  set; }
        [NotNull]
        public virtual EnumStatus Status { get;  set; }

        [CanBeNull]
        public virtual string? Note { get; set; }        

        public virtual Guid AppointmentTypeId { get; set; }
        public virtual Guid DepartmentId { get; set; }
        public virtual Guid DoctorId { get; set; }
        public virtual Guid PatientId { get; set; }

        protected Appointment()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Note = string.Empty;
        }

        public Appointment(Guid id, Guid appointmentTypeId, 
            Guid departmentId, Guid doctorId, 
            Guid patientId, DateTime startTime,
            DateTime endTime, string note,
            EnumStatus status = default)
        {
            Check.NotNullOrWhiteSpace(appointmentTypeId.ToString(), nameof(appointmentTypeId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(startTime, nameof(startTime));
            Check.NotNull(endTime, nameof(endTime));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.Length(note, nameof(note), AppointmentConsts.NoteMaxLength);
                       

            Id = id;
            AppointmentTypeId = appointmentTypeId;
            DepartmentId = departmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            StartTime = startTime;
            EndTime = endTime;
            Note = note;
            Status = status;
           
        }

    }

}

