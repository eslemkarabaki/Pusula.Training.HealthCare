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
            DateTime appointmentStartDate, DateTime appointmentEndDate,
            EnumStatus status, string? notes = null)
        {
            Check.NotNullOrWhiteSpace(appointmentTypeId.ToString(), nameof(appointmentTypeId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(appointmentStartDate, nameof(appointmentStartDate));
            Check.NotNull(appointmentEndDate, nameof(appointmentEndDate));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.Length(notes, nameof(notes), AppointmentConsts.NotesMaxLength, AppointmentConsts.NotesMinLength);

            var appointment = new Appointment(
                GuidGenerator.Create(),
                appointmentTypeId, departmentId, doctorId, patientId, appointmentStartDate, appointmentEndDate, notes!, status!);

            return await appointmentRepository.InsertAsync(appointment);
        }
        #endregion

        #region UpdateAsync
        public virtual async Task<Appointment> UpdateAsync(Guid id, 
            Guid appointmentTypeId, Guid departmentId,
            Guid doctorId, Guid patientId,
            DateTime appointmentStartDate, DateTime appointmentEndDate,
            EnumStatus status, string? notes = null, 
            [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(appointmentTypeId.ToString(), nameof(appointmentTypeId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(appointmentStartDate, nameof(appointmentStartDate));
            Check.NotNull(appointmentEndDate, nameof(appointmentEndDate));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.Length(notes, nameof(notes), AppointmentConsts.NotesMaxLength, AppointmentConsts.NotesMinLength);

            var appointment = await appointmentRepository.GetAsync(id);

            appointment.AppointmentTypeId = appointmentTypeId;
            appointment.DepartmentId = departmentId;
            appointment.DoctorId = doctorId;
            appointment.PatientId = patientId;
            appointment.AppointmentStartDate = appointmentStartDate;
            appointment.AppointmentEndDate = appointmentEndDate;
            appointment.Status = status;
            appointment.Notes = notes;            

            appointment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentRepository.UpdateAsync(appointment);
        }
        #endregion
    }
}
