using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        [JetBrains.Annotations.NotNull]
        public virtual DateTime AppointmentDate { get; set; }
        [JetBrains.Annotations.NotNull]
        public virtual EnumStatus Status { get; set; }

        [CanBeNull]
        public virtual string? Notes { get; set; }

        public virtual Guid HospitalId { get; set; }
        public virtual Guid DepartmentId { get; set; }
        public virtual Guid DoctorId { get; set; }
        public virtual Guid PatientId { get; set; }

        protected Appointment()
        {
            AppointmentDate = DateTime.Now;
            Notes = string.Empty;
        }

        public Appointment(Guid id, Guid hospitalId, 
            Guid departmentId, Guid doctorId, 
            Guid patientId, DateTime appointmentDate, 
            string notes, EnumStatus status = default)
        {
            Check.NotNullOrWhiteSpace(hospitalId.ToString(), nameof(hospitalId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(appointmentDate, nameof(appointmentDate));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.NotNullOrWhiteSpace(notes, nameof(notes));

            Id = id;
            HospitalId = hospitalId;
            DepartmentId = departmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            AppointmentDate = appointmentDate;
            Status = status;
            Notes = notes;
        }

    }

}

