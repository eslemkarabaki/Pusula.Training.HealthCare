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
using Pusula.Training.HealthCare.Patients;
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
        string? notes = null, EnumAppointmentStatus? status = null,
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
        return await (await GetQueryForNavigationPropertiesAsync()).FirstOrDefaultAsync(b => b.Appointment.Id == id);            
    }
    #endregion

    #region GetListWithNavigationProperties
    public virtual async Task<List<AppointmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
         string? filterText = null,
        DateTime? startTime = null, DateTime? endTime = null,
        string? note = null, EnumAppointmentStatus? status = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        string? sorting = null, int maxResultCount = int.MaxValue,
        int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, startTime, endTime, status, note, appointmentTypeId, departmentId, doctorId, patientId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }
    #endregion

    #region GetQueryForNavigationProperties
    protected virtual async Task<IQueryable<AppointmentWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        var dbContext = await GetDbContextAsync();
        return from appointment in dbContext.Appointments
               join appointmentType in dbContext.AppointmentTypes on appointment.AppointmentTypeId equals appointmentType.Id into appointmentTypes
               from appointmentType in appointmentTypes.DefaultIfEmpty()
               join department in dbContext.Departments on appointment.DepartmentId equals department.Id into departments
               from department in departments.DefaultIfEmpty()
               join doctor in dbContext.Doctors on appointment.DoctorId equals doctor.Id into doctors
               from doctor in doctors.DefaultIfEmpty()
               join patient in dbContext.Patients on appointment.PatientId equals patient.Id into patients
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
        string? filterText = null, DateTime? startTime = null, DateTime? endTime =null,
        EnumAppointmentStatus? status = null, string? note = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null)
    {
        return query
                .WhereIf(startTime.HasValue, e => e.Appointment.StartTime >= startTime!.Value)
                .WhereIf(endTime.HasValue, e => e.Appointment.EndTime <= endTime!.Value)
                .WhereIf(status.HasValue && status != 0, e => e.Appointment.Status == status!.Value)
                .WhereIf(appointmentTypeId.HasValue, e => e.AppointmentType.Id == appointmentTypeId!.Value)
                .WhereIf(departmentId.HasValue, e => e.Department.Id == departmentId!.Value)
                .WhereIf(doctorId.HasValue, e => e.Doctor.Id == doctorId!.Value)
                .WhereIf(patientId.HasValue, e => e.Patient.Id == patientId!.Value);
    }
    #endregion

    #region GetList
    public virtual async Task<List<Appointment>> GetListAsync(
        string? filterText = null,
        DateTime? startTime = null, DateTime? endTime = null,
        string? note = null, EnumAppointmentStatus? status = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        string? sorting = null, int maxResultCount = int.MaxValue,
        int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter(await GetQueryableAsync(), filterText, doctorId, startTime, endTime, status, note);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? AppointmentConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }
    #endregion

    #region GetCount
    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        DateTime? startTime = null, DateTime? endTime = null,
        string? note = null, EnumAppointmentStatus? status = null,
        Guid? appointmentTypeId = null, Guid? departmentId = null,
        Guid? doctorId = null, Guid? patientId = null,
        CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, startTime, endTime, status, note, appointmentTypeId, departmentId, doctorId, patientId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }
    #endregion

    #region ApplyFilter
    protected virtual IQueryable<Appointment> ApplyFilter(
    IQueryable<Appointment> query,
    string? filterText = null, Guid? doctorId = null, DateTime? startTime = null, DateTime? endTime = null,
    EnumAppointmentStatus? status = null, string? note = null)
{
    return query
        .WhereIf(doctorId.HasValue, e=>e.DoctorId==doctorId!.Value)
        .WhereIf(status.HasValue, e => e.Status == status!.Value)
        .WhereIf(startTime.HasValue, e => e.StartTime >= startTime!.Value)
        .WhereIf(endTime.HasValue, e => e.EndTime <= endTime!.Value)
        .WhereIf(!string.IsNullOrWhiteSpace(note), e => e.Note!.Contains(note!));
}
    #endregion
}
