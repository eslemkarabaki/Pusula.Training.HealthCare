using JetBrains.Annotations;
using Pusula.Training.HealthCare.AppointmentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentManager(IAppointmentRepository appointmentRepository):DomainService
    {
        #region CreateAsync
        public virtual async Task<Appointment> CreateAsync(
            Guid appointmentTypeId,Guid departmentId, 
            Guid doctorId, Guid patientId, 
            DateTime startTime, DateTime endTime,
            EnumStatus status, string? note = null)
        {
            Check.NotNullOrWhiteSpace(appointmentTypeId.ToString(), nameof(appointmentTypeId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(startTime, nameof(startTime));
            Check.NotNull(endTime, nameof(endTime));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.Length(note, nameof(note), AppointmentConsts.NoteMaxLength);

            var appointment = new Appointment(
                GuidGenerator.Create(),
                appointmentTypeId, departmentId, doctorId, patientId, startTime, endTime, note!, status!);

            return await appointmentRepository.InsertAsync(appointment);
        }
        #endregion

        #region UpdateAsync
        public virtual async Task<Appointment> UpdateAsync(Guid id, 
            Guid appointmentTypeId, Guid departmentId,
            Guid doctorId, Guid patientId,
            DateTime startTime, DateTime endTime,
            EnumStatus status, string? note = null, 
            [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(appointmentTypeId.ToString(), nameof(appointmentTypeId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(startTime, nameof(startTime));
            Check.NotNull(endTime, nameof(endTime));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.Length(note, nameof(note), AppointmentConsts.NoteMaxLength);

            var appointment = await appointmentRepository.GetAsync(id);

            appointment.AppointmentTypeId = appointmentTypeId;
            appointment.DepartmentId = departmentId;
            appointment.DoctorId = doctorId;
            appointment.PatientId = patientId;
            appointment.StartTime = startTime;
            appointment.EndTime = endTime;
            appointment.Status = status;
            appointment.Note = note;            

            appointment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentRepository.UpdateAsync(appointment);
        }
        #endregion
    }
}
