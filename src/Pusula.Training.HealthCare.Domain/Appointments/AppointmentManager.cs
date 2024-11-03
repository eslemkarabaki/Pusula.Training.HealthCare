using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentManager(IAppointmentRepository appointmentRepository):DomainService
    {
        public virtual async Task<Appointment> CreateAsync(
            Guid hospitalId,Guid departmentId, 
            Guid doctorId, Guid patientId, 
            DateTime appointmentDate, EnumStatus status,
            string? notes = null)
        {
            Check.NotNullOrWhiteSpace(hospitalId.ToString(), nameof(hospitalId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(appointmentDate, nameof(appointmentDate));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.NotNullOrWhiteSpace(notes, nameof(notes));

            var appointment = new Appointment(
                GuidGenerator.Create(),
                hospitalId, departmentId, doctorId, patientId, appointmentDate, notes, status);

            return await appointmentRepository.InsertAsync(appointment);
        }

        public virtual async Task<Appointment> UpdateAsync(
            Guid id, Guid hospitalId, Guid departmentId,
            Guid doctorId, Guid patientId,
            DateTime appointmentDate, EnumStatus status,
            string? notes = null, [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(hospitalId.ToString(), nameof(hospitalId));
            Check.NotNullOrWhiteSpace(departmentId.ToString(), nameof(departmentId));
            Check.NotNullOrWhiteSpace(doctorId.ToString(), nameof(doctorId));
            Check.NotNullOrWhiteSpace(patientId.ToString(), nameof(patientId));
            Check.NotNull(appointmentDate, nameof(appointmentDate));
            Check.Range((int)status, nameof(status), 1, 5);
            Check.NotNullOrWhiteSpace(notes, nameof(notes));

            var appointment = await appointmentRepository.GetAsync(id);

            appointment.HospitalId = hospitalId;
            appointment.DepartmentId = departmentId;
            appointment.DoctorId = doctorId;
            appointment.PatientId = patientId;
            appointment.AppointmentDate = appointmentDate;
            appointment.Notes = notes;
            appointment.Status = status;

            appointment.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await appointmentRepository.UpdateAsync(appointment);
        }
    }
}
