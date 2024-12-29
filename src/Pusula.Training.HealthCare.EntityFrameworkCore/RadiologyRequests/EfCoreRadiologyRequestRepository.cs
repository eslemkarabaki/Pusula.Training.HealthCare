using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.RadiologyRequests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.ProtocolTypes;

public class EfCoreRadiologyRequestRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, RadiologyRequest, Guid>(dbContextProvider), IRadiologyRequestRepository
{
    #region DeleteAllAsync
    public virtual async Task DeleteAllAsync
        (
            string? filterText = null,
            DateTime? requestDate = null,
            Guid? protocolId = null,
            Guid? departmentId = null,
            Guid? doctorId = null,
            CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, requestDate, protocolId, departmentId, doctorId);

        var ids = query.Select(x => x.RadiologyRequest.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetCountAsync
    public virtual async Task<long> GetCountAsync
        (
            string? filterText = null,
            DateTime? requestDate = null,
            Guid? protocolId = null,
            Guid? departmentId = null,
            Guid? doctorId = null,
            CancellationToken cancellationToken = default
        )
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, requestDate, protocolId, departmentId, doctorId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetListAsync
    public virtual async Task<List<RadiologyRequest>> GetListAsync
        (
            string? filterText = null,
            DateTime? requestDate = null,
            Guid? protocolId = null,
            Guid? departmentId = null,
            Guid? doctorId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        )
    {
        var query = ApplyFilter(await GetQueryableAsync(), filterText, requestDate, protocolId, departmentId, doctorId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RadiologyRequestConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetListRadiologyRequestWithNavigationPropertiesAsync
    public virtual async Task<List<RadiologyRequestWithNavigationProperties>> GetListRadiologyRequestWithNavigationPropertiesAsync
        (
            string? filterText = null,
            DateTime? requestDate = null,
            Guid? protocolId = null,
            Guid? departmentId = null,
            Guid? doctorId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        )
    {
        var query =  await GetQueryForNavigationPropertiesAsync(); 
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RadiologyRequestConsts.GetDefaultSorting(false) : sorting); 
        return await query.ToListAsync(GetCancellationToken(cancellationToken));
    }
    #endregion

    #region GetWithNavigationPropertiesAsync
    public virtual async Task<RadiologyRequestWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbConext = await GetDbContextAsync();

        return (await GetDbSetAsync())
            .Where(r => r.Id == id)
            .Select(r => new RadiologyRequestWithNavigationProperties
            {
                RadiologyRequest = r,
                Protocol = dbConext.Set<Protocol>().FirstOrDefault(p => p.Id == r.ProtocolId),
                Department = dbConext.Set<Department>().FirstOrDefault(d => d.Id == r.DepartmentId),
                Doctor = dbConext.Set<Doctor>().FirstOrDefault(d => d.Id == r.DoctorId)
            })
            .FirstOrDefault()!;
    }
    #endregion


    #region GetQueryForNavigationPropertiesAsync
    protected virtual async Task<IQueryable<RadiologyRequestWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        var dbContext = await GetDbContextAsync();
        var dbSet = await GetDbSetAsync();

        var query = from requestItem in dbSet
                    join protocol in dbContext.Set<Protocol>() on requestItem.ProtocolId equals protocol.Id into protocols
                    from protocol in protocols.DefaultIfEmpty()
                    join department in dbContext.Set<Department>() on requestItem.DepartmentId equals department.Id into departments
                    from department in departments.DefaultIfEmpty()
                    join doctor in dbContext.Set<Doctor>() on requestItem.DoctorId equals doctor.Id into doctors
                    from doctor in doctors.DefaultIfEmpty()
                    join patient in dbContext.Set<Patient>() on protocol.PatientId equals patient.Id into patients
                    from patient in patients.DefaultIfEmpty()
                    join protocolType in dbContext.Set<ProtocolType>() on protocol.ProtocolTypeId equals protocolType.Id into protocolTypes
                    from protocolType in protocolTypes.DefaultIfEmpty()
                    select new RadiologyRequestWithNavigationProperties
                    {
                        RadiologyRequest = requestItem,
                        Protocol = protocol,
                        Department = department,
                        Doctor = doctor,
                        Patient = patient,
                        ProtocolType = protocolType
                    };

        var result = await query.ToListAsync();
        return query;
    }

    #endregion


    #region RadiologyRequestWithNavigationProperties ApplyFilter
    public virtual IQueryable<RadiologyRequestWithNavigationProperties> ApplyFilter(
        IQueryable<RadiologyRequestWithNavigationProperties> query,
        string? filterText = null,
        DateTime? requestDate = null,
        Guid? protocolId = null,
        Guid? departmentId = null,
        Guid? doctorId = null)
    {
        query = query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),
                x => x.RadiologyRequest.RequestDate.ToString().Contains(filterText!))  
            .WhereIf(requestDate.HasValue,
                x => x.RadiologyRequest.RequestDate == requestDate!.Value)  
            .WhereIf(protocolId.HasValue,
                e => e.RadiologyRequest.ProtocolId == protocolId!.Value) 
            .WhereIf(departmentId.HasValue,
                e => e.RadiologyRequest.DepartmentId == departmentId!.Value)  
            .WhereIf(doctorId.HasValue,
                e => e.RadiologyRequest.DoctorId == doctorId!.Value);    

        return query;
    }

    #endregion

    #region ApplyFilter
    public virtual IQueryable<RadiologyRequest> ApplyFilter(
        IQueryable<RadiologyRequest> query,
        string? filterText = null,
        DateTime? requestDate = null,
        Guid? protocolId = null,
        Guid? departmentId = null,
        Guid? doctorId = null)
    {
        return query;
    }
    #endregion
}
