using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
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
            EnumAppointmentStatus status, string? note = null)
        {           

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
            EnumAppointmentStatus status, string? note = null, 
            [CanBeNull] string? concurrencyStamp = null)
        {           
            var appointment = await appointmentRepository.GetAsync(id);

            appointment.SetAppointmentTypeId(appointmentTypeId);
            appointment.SetDepartmentId(departmentId);
            appointment.SetDoctorId(doctorId);
            appointment.SetPatientId(patientId);
            appointment.SetStartTime(startTime);
            appointment.SetEndTime(endTime);
            appointment.SetStatus(status);
            appointment.SetNote(note);           

            appointment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentRepository.UpdateAsync(appointment);
        }

        public virtual async Task<Appointment> UpdateDateAsync(Guid id,            
            DateTime startTime, DateTime endTime,            
            [CanBeNull] string? concurrencyStamp = null)
        {
            var appointment = await appointmentRepository.GetAsync(id);            
            appointment.SetStartTime(startTime);
            appointment.SetEndTime(endTime);
            
            appointment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentRepository.UpdateAsync(appointment);
        }
        #endregion
    }
}
