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
using System.Reflection;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.AppointmentTypes;

namespace Pusula.Training.HealthCare.Appointments;

public class EfCoreAppointmentRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Appointment, Guid>(dbContextProvider), IAppointmentRepository

{
    #region DeleteAll
    public virtual async Task DeleteAllAsync(
        string? filterText = null,
        DateTime? appointmentStartDate = null, DateTime? appointmentEndDate = null,
        string? notes = null, EnumStatus? status = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, appointmentStartDate, appointmentEndDate, status, notes, appointmentTypeId, departmentId, doctorId, patientId);

        var ids = query.Select(x => x.Appointment.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetWithNavigationProperties
    public virtual async Task<AppointmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync()).Where(b => b.Id == id)
            .Select(appointment => new AppointmentWithNavigationProperties
            {
                Appointment = appointment,
                AppointmentType = dbContext.Set<AppointmentType>().FirstOrDefault(c => c.Id == appointment.AppointmentTypeId)!,
                Department = dbContext.Set<Department>().FirstOrDefault(c => c.Id == appointment.DepartmentId)!,
                Doctor = dbContext.Set<Doctor>().FirstOrDefault(c => c.Id == appointment.DoctorId)!,
                Patient = dbContext.Set<Patient>().FirstOrDefault(c => c.Id == appointment.PatientId)!,

            }).FirstOrDefault()!;
    }
    #endregion

    #region GetListWithNavigationProperties
    public virtual async Task<List<AppointmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
         string? filterText = null,
        DateTime? appointmentStartDate = null, DateTime? appointmentEndDate = null,
        string? notes = null, EnumStatus? status = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        string? sorting = null, int maxResultCount = int.MaxValue,
        int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, appointmentStartDate, appointmentEndDate, status, notes, appointmentTypeId, departmentId, doctorId, patientId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }
    #endregion

    #region GetQueryForNavigationProperties
    protected virtual async Task<IQueryable<AppointmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from appointment in (await GetDbSetAsync())
               join appointmentType in (await GetDbContextAsync()).Set<AppointmentType>() on appointment.AppointmentTypeId equals appointmentType.Id into appointmentTypes
               from appointmentType in appointmentTypes.DefaultIfEmpty()
               join department in (await GetDbContextAsync()).Set<Department>() on appointment.DepartmentId equals department.Id into departments
               from department in departments.DefaultIfEmpty()
               join doctor in (await GetDbContextAsync()).Set<Doctor>() on appointment.DoctorId equals doctor.Id into doctors
               from doctor in doctors.DefaultIfEmpty()
               join patient in (await GetDbContextAsync()).Set<Patient>() on appointment.PatientId equals patient.Id into patients
               from patient in patients.DefaultIfEmpty()
               select new AppointmentWithNavigationProperties
               {
                   Appointment = appointment,
                   AppointmentType = appointmentType,
                   Department = department,
                   Doctor = doctor,
                   Patient = patient,
               };
    }
    #endregion

    #region ApplyFiterWithNavigationProperties
    protected virtual IQueryable<AppointmentWithNavigationProperties> ApplyFilter(
        IQueryable<AppointmentWithNavigationProperties> query,
        string? filterText = null, DateTime? appointmentStartDate = null, DateTime? appointmentEndDate =null,
        EnumStatus? status = null, string? notes = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null)
    {
        return query
               //.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Appointment.AppointmentDate!.Contains(filterText!) || e.Appointment.Status!.Contains(filterText!))
                .WhereIf(appointmentStartDate.HasValue, e => e.Appointment.AppointmentStartDate >= appointmentStartDate!.Value)
                .WhereIf(appointmentEndDate.HasValue, e => e.Appointment.AppointmentEndDate >= appointmentEndDate!.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(notes), e => e.Appointment.Notes!.Contains(notes!))
                .WhereIf(status.HasValue, e => e.Appointment.Status == status!.Value) 
                .WhereIf(appointmentTypeId != null && appointmentTypeId != Guid.Empty, e => e.AppointmentType != null && e.AppointmentType.Id == appointmentTypeId)
                .WhereIf(departmentId != null && departmentId != Guid.Empty, e => e.Department != null && e.Department.Id == departmentId)
                .WhereIf(doctorId != null && doctorId != Guid.Empty, e => e.Doctor != null && e.Doctor.Id == doctorId)
                .WhereIf(patientId != null && patientId != Guid.Empty, e => e.Patient != null && e.Patient.Id == patientId);
    }
    #endregion

    #region GetList
    public virtual async Task<List<Appointment>> GetListAsync(
        string? filterText = null,
        DateTime? appointmentStartDate = null, DateTime? appointmentEndDate = null,
        string? notes = null, EnumStatus? status = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        string? sorting = null, int maxResultCount = int.MaxValue,
        int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter(await GetQueryableAsync(), filterText, appointmentStartDate, appointmentEndDate, status, notes);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }
    #endregion

    #region GetCount
    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        DateTime? appointmentStartDate = null, DateTime? appointmentEndDate = null,
        string? notes = null, EnumStatus? status = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, appointmentStartDate, appointmentEndDate, status, notes, appointmentTypeId, departmentId, doctorId, patientId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }
    #endregion

    #region ApplyFilter
    protected virtual IQueryable<Appointment> ApplyFilter(
    IQueryable<Appointment> query,
    string? filterText = null, DateTime? appointmentStartDate = null, DateTime? appointmentEndDate = null,
    EnumStatus? status = null, string? notes = null)
{
    return query
        //.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.AppointmentDate.ToString().Contains(filterText!) || e.Status.ToString().Contains(filterText!))
        .WhereIf(status.HasValue, e => e.Status == status!.Value)
        .WhereIf(appointmentStartDate.HasValue, e => e.AppointmentStartDate >= appointmentStartDate!.Value)
        .WhereIf(appointmentEndDate.HasValue, e => e.AppointmentEndDate >= appointmentEndDate!.Value)
        .WhereIf(!string.IsNullOrWhiteSpace(notes), e => e.Notes!.Contains(notes!));
}
    #endregion
}
