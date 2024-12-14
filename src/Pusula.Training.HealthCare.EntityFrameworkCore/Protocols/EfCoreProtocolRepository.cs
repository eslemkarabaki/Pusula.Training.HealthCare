using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Protocols;

public class EfCoreProtocolRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Protocol, Guid>(dbContextProvider), IProtocolRepository
{
    public async Task<List<Protocol>> GetListAsync(
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                  await GetQueryableAsync(), string.Empty, patientId, doctorId, departmentId, protocolTypeId, status,
                  startTime, endTime
              )
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<List<Protocol>> GetListWithDetailsAsync(
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                  await WithDetailsAsync(e => e.Patient, e => e.Department, e => e.Doctor, e => e.ProtocolType),
                  string.Empty, patientId, doctorId, departmentId, protocolTypeId, status,
                  startTime, endTime
              )
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<long> GetCountAsync(
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                await GetQueryableAsync(),
                string.Empty, patientId, doctorId, departmentId, protocolTypeId, status,
                startTime, endTime
            )
            .LongCountAsync(GetCancellationToken(cancellationToken));

    protected virtual IQueryable<Protocol> ApplyFilter(
        IQueryable<Protocol> query,
        string? filterText = null, // todo
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null
    ) =>
        query
            .WhereIf(patientId.HasValue, e => e.PatientId == patientId!.Value)
            .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId!.Value)
            .WhereIf(departmentId.HasValue, e => e.DepartmentId == departmentId!.Value)
            .WhereIf(protocolTypeId.HasValue, e => e.ProtocolTypeId == protocolTypeId!.Value)
            .WhereIf(status != EnumProtocolStatus.None, e => e.Status == status)
            .WhereIf(startTime.HasValue, e => e.StartTime >= startTime!.Value)
            .WhereIf(endTime.HasValue, e => !e.EndTime.HasValue || e.EndTime <= endTime!.Value);

    protected virtual string GetSorting(string? sorting, bool withEntityName) =>
        sorting.IsNullOrWhiteSpace() ?
            ProtocolConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "Protocol." : string.Empty)}{sorting}";
}