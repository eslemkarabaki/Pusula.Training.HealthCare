using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Doctors;

public class EfCoreDoctorRepository : EfCoreRepository<HealthCareDbContext, Doctor, Guid>, IDoctorRepository
{
    public EfCoreDoctorRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Doctor> GetAsync(Guid? doctorId, Guid? userId, CancellationToken cancellationToken = default) =>
        await (await GetQueryableAsync())
              .WhereIf(doctorId.HasValue, x => x.Id == doctorId!.Value)
              .WhereIf(userId.HasValue, x => x.UserId == userId!.Value)
              .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));

    public virtual async Task DeleteAllAsync(
        string? filterText = null,
        string? fullname = null,
        int? appointmentTime = null,
        Guid? titleId = null,
        Guid? departmentId = null,
        Guid? hospitalId = null,
        CancellationToken cancellationToken = default
    )
    {
        var query = await GetQueryableAsync();

        // Apply filters to the query
        query = ApplyFilter(
            query, filterText, fullname, appointmentTime, titleId, departmentId, hospitalId
        );

        // Select doctor IDs to delete
        var ids = query.Select(x => x.Id);

        // Delete the selected doctors
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Doctor>> GetListAsync(
        string? filterText = null,
        string? fullname = null,
        int? appointmentTime = null,
        Guid? titleId = null,
        Guid? departmentId = null,
        Guid? hospitalId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    )
    {
        var query = ApplyFilter(
            await GetQueryableAsync(), filterText, fullname, appointmentTime, titleId, departmentId,
            hospitalId
        );

        // Apply sorting if provided
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorConsts.GetDefaultSorting(false) : sorting);

        // Paginate the result set
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<List<DoctorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        string? fullname = null,
        int? appointmentTime = null,
        Guid? titleId = null,
        Guid? departmentId = null,
        Guid? hospitalId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    )
    {
        var query = ApplyFilter(
            await GetQueryForNavigationPropertiesAsync(), filterText, fullname, appointmentTime,
            titleId, departmentId, hospitalId
        );

        // Apply sorting if provided
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorConsts.GetDefaultSorting(true) : sorting);

        // Paginate the result set
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        string? fullname = null,
        int? appointmentTime = null,
        Guid? titleId = null,
        Guid? departmentId = null,
        Guid? hospitalId = null,
        CancellationToken cancellationToken = default
    )
    {
        var query = ApplyFilter(
            await GetQueryableAsync(), filterText, fullname, appointmentTime, titleId, departmentId,
            hospitalId
        );
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<DoctorWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        var dbContext = await GetDbContextAsync();

        return
            from doctor in dbContext.Doctors
            join title in dbContext.Titles
                on doctor.TitleId equals title.Id
            join department in dbContext.Departments
                on doctor.DepartmentId equals department.Id
            join hospital in dbContext.Hospitals
                on doctor.HospitalId equals hospital.Id
            select new DoctorWithNavigationProperties
            {
                Doctor = doctor,
                Title = title,
                Department = department,
                Hospital = hospital
            };
    }

    // Helper method to apply filters on the query
    private IQueryable<Doctor> ApplyFilter(
        IQueryable<Doctor> query,
        string? filterText = null,
        string? fullName = null,
        int? appointmentTime = null,
        Guid? titleId = null,
        Guid? departmentId = null,
        Guid? hospitalId = null
    ) =>
        query
            .WhereIf(
                !string.IsNullOrWhiteSpace(filterText),
                e => EF.Functions.ILike(e.FullName, $"{filterText!}%")
            )
            .WhereIf(!string.IsNullOrWhiteSpace(fullName), e => EF.Functions.ILike(e.FullName, $"{fullName!}%"))
            .WhereIf(appointmentTime.HasValue, e => e.AppointmentTime == appointmentTime!.Value)
            .WhereIf(titleId.HasValue, e => e.TitleId == titleId!.Value)
            .WhereIf(departmentId.HasValue, e => e.DepartmentId == departmentId!.Value)
            .WhereIf(hospitalId.HasValue, e => e.HospitalId == hospitalId!.Value);

    private IQueryable<DoctorWithNavigationProperties> ApplyFilter(
        IQueryable<DoctorWithNavigationProperties> query,
        string? filterText = null,
        string? fullName = null,
        int? appointmentTime = null,
        Guid? titleId = null,
        Guid? departmentId = null,
        Guid? hospitalId = null
    ) =>
        query
            .WhereIf(
                !string.IsNullOrWhiteSpace(filterText),
                e => EF.Functions.ILike(e.Doctor.FullName, $"{filterText!}%")
            )
            .WhereIf(!string.IsNullOrWhiteSpace(fullName), e => EF.Functions.ILike(e.Doctor.FullName, $"{fullName!}%"))
            .WhereIf(appointmentTime.HasValue, e => e.Doctor.AppointmentTime == appointmentTime!.Value)
            .WhereIf(titleId.HasValue, e => e.Doctor.TitleId == titleId!.Value)
            .WhereIf(departmentId.HasValue, e => e.Doctor.DepartmentId == departmentId!.Value)
            .WhereIf(hospitalId.HasValue, e => e.Doctor.HospitalId == hospitalId!.Value);
}