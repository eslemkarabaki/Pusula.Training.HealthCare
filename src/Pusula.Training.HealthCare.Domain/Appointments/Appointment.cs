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
        public virtual DateTime StartTime { get; private  set; }
        [NotNull]
        public virtual DateTime EndTime { get; private set; }
        [NotNull]
        public virtual EnumStatus Status { get; private set; }
        [CanBeNull]
        public virtual string? Note { get; private set; }     
        public virtual Guid AppointmentTypeId { get; private set; }
        public virtual Guid DepartmentId { get; private set; }
        public virtual Guid DoctorId { get; private set; }
        public virtual Guid PatientId { get; private set; }

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
            SetAppointmentTypeId(appointmentTypeId);
            SetDepartmentId(departmentId);
            SetDoctorId(doctorId);
            SetPatientId(patientId);
            SetStartTime(startTime);
            SetEndTime(endTime);
            SetStatus(status);
            SetNote(note);      
           
        }
        public void SetAppointmentTypeId(Guid appointmentTypeId) => AppointmentTypeId = Check.NotDefaultOrNull<Guid>(appointmentTypeId, nameof(appointmentTypeId));
        public void SetDepartmentId(Guid departmentId) => DepartmentId = Check.NotDefaultOrNull<Guid>(departmentId, nameof(departmentId));
        public void SetDoctorId(Guid doctorId) => DoctorId = Check.NotDefaultOrNull<Guid>(doctorId, nameof(doctorId));
        public void SetPatientId(Guid patientId) => PatientId = Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId)); 
        public void SetStartTime(DateTime startTime) => StartTime = Check.NotNull(startTime, nameof(startTime));
        public void SetEndTime(DateTime endTime) => EndTime = Check.NotNull(endTime,nameof(endTime));
        public void SetStatus(EnumStatus status) => Status = status;
        public void SetNote(string note) => Note = Check.Length(note, nameof(note), AppointmentConsts.NoteMaxLength);

    }

}