using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using Pusula.Training.HealthCare.Departments;
using System.Numerics;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using System.Reflection;

namespace Pusula.Training.HealthCare.Appointments
{
    public class EfCoreAppointmentRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : EfCoreRepository<HealthCareDbContext, Appointment, Guid>(dbContextProvider), IAppointmentRepository

    {
        public virtual async Task DeleteAllAsync(
            string? filterText = null, DateTime? appointmentDate = null,
            EnumStatus? status = null, string? notes = null,
            Guid? hospitalId = null, Guid? departmentId = null,
            Guid? doctorId = null, Guid? patientId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query=ApplyFilter(query, filterText, appointmentDate, status, notes, hospitalId, departmentId, doctorId, patientId);

            var ids=query.Select(x=>x.Appointment.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

    public virtual async Task<AppointmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken= default)
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync()).Where(b => b.Id == id)
            .Select(appointment => new AppointmentWithNavigationProperties
            {
                Appointment = appointment,
                Hospital = dbContext.Set<Hospital>().FirstOrDefault(c => c.Id == appointment.HospitalId)!,
                Department = dbContext.Set<Department>().FirstOrDefault(c => c.Id == appointment.DepartmentId)!,
                Doctor = dbContext.Set<Doctor>().FirstOrDefault(c => c.Id == appointment.DoctorId)!,
                Patient = dbContext.Set<Patient>().FirstOrDefault(c => c.Id == appointment.PatientId)!,

            }).FirstOrDefault()!;
    }

    public virtual async Task<List<AppointmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText, DateTime? appointmentDate = null,
            EnumStatus? status = null, string? notes = null,
            Guid? hospitalId = null, Guid? departmentId = null,
            Guid? doctorId = null, Guid? patientId = null,
            string? sorting = null, int maxResultCount = int.MaxValue,
            int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, appointmentDate, status, notes, hospitalId, departmentId, doctorId, patientId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<AppointmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from appointment in (await GetDbSetAsync())
               join hospital in (await GetDbContextAsync()).Set<Hospital>() on appointment.HospitalId equals hospital.Id into hospitals
               from hospital in hospitals.DefaultIfEmpty()
               join department in (await GetDbContextAsync()).Set<Department>() on appointment.DepartmentId equals department.Id into departments
               from department in departments.DefaultIfEmpty()
               join doctor in (await GetDbContextAsync()).Set<Doctor>() on appointment.DoctorId equals doctor.Id into doctors
               from doctor in doctors.DefaultIfEmpty()
               join patient in (await GetDbContextAsync()).Set<Patient>() on appointment.PatientId equals patient.Id into patients
               from patient in patients.DefaultIfEmpty()
               select new AppointmentWithNavigationProperties
               {
                   Appointment = appointment,
                   Hospital = hospital,
                   Department = department,
                   Doctor = doctor,
                   Patient = patient,
               };

    }

    protected virtual IQueryable<AppointmentWithNavigationProperties> ApplyFilter(
        IQueryable<AppointmentWithNavigationProperties> query,
        string? filterText = null, DateTime? appointmentDate = null,
        EnumStatus? status = null, string? notes = null,
        Guid? hospitalId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Appointment.AppointmentDate!.Contains(filterText!) || e.Appointment.Status!.Contains(filterText!))
                .WhereIf(appointmentDate.HasValue, e => e.Appointment.AppointmentDate >= appointmentDate!.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(notes), e => e.Appointment.Notes.Contains(notes!))
                .WhereIf(status.HasValue, e => e.Status == status)
                .WhereIf(hospitalId != null && hospitalId != Guid.Empty, e => e.Hospital != null && e.Hospital.Id == hospitalId)
                .WhereIf(departmentId != null && departmentId != Guid.Empty, e => e.Department != null && e.Department.Id == departmentId)
                .WhereIf(doctorId != null && doctorId != Guid.Empty, e => e.Doctor != null && e.Doctor.Id == doctorId)
                .WhereIf(patientId != null && patientId != Guid.Empty, e => e.Patient != null && e.Patient.Id == patientId);
    }

    public virtual async Task<List<Appointment>> GetListAsync(
        string? filterText, DateTime? appointmentDate = null,
            EnumStatus? status = null, string? notes = null,
            Guid? hospitalId = null, Guid? departmentId = null,
            Guid? doctorId = null, Guid? patientId = null,
            string? sorting = null, int maxResultCount = int.MaxValue,
            int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, appointmentDate, status, notes);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText, DateTime? appointmentDate = null,
            EnumStatus? status = null, string? notes = null,
            Guid? hospitalId = null, Guid? departmentId = null,
            Guid? doctorId = null, Guid? patientId = null,
            CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, appointmentDate, status, notes, hospitalId, departmentId, doctorId, patientId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<Appointment> ApplyFilter(
        IQueryable<Appointment> query,
        string? fiterText = null, DateTime? appointmentDate=null,
        EnumStatus? status=null, string? notes = null)
    {
        return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Appointment.AppointmentDate!.Contains(filterText!) || e.Appointment.Status!.Contains(filterText!))
                .WhereIf(appointmentDate.HasValue, e => e.Appointment.AppointmentDate >= appointmentDate!.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(notes), e => e.Appointment.Notes.Contains(notes!))
                .WhereIf(status.HasValue, e => e.Status == status);
                
    }
}
