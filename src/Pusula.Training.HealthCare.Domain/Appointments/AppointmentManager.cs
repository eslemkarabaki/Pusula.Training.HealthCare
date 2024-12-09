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
