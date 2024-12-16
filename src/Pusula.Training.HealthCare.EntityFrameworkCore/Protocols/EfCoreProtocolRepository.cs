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
        Guid? protocolTypeActionId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                  await GetQueryableAsync(), string.Empty, patientId, doctorId, departmentId, protocolTypeId,
                  protocolTypeActionId, status,
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
        Guid? protocolTypeActionId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                  await WithDetailsAsync(
                      e => e.Patient, e => e.Department, e => e.Doctor, e => e.ProtocolType, e => e.ProtocolTypeAction
                  ),
                  string.Empty, patientId, doctorId, departmentId, protocolTypeId, protocolTypeActionId, status,
                  startTime, endTime
              )
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<List<Protocol>> GetDoctorWorkListWithDetailsAsync(
        Guid userId,
        ICollection<EnumProtocolStatus>? status = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilterForDoctorWorkList(
                  await WithDetailsAsync(
                      e => e.Patient, e => e.Department, e => e.Doctor, e => e.ProtocolType, e => e.ProtocolTypeAction
                  ), userId, status, startTime, endTime
              )
              .OrderBy(GetSorting(sorting, false))
              .Skip(skipCount)
              .Take(maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<long> GetCountAsync(
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        Guid? protocolTypeActionId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                await GetQueryableAsync(),
                string.Empty, patientId, doctorId, departmentId, protocolTypeId, protocolTypeActionId, status,
                startTime, endTime
            )
            .LongCountAsync(GetCancellationToken(cancellationToken));

    public async Task<long> GetCountForDoctorWorkListAsync(
        Guid userId,
        ICollection<EnumProtocolStatus>? status = null,
        DateTime? startTime = null,
        DateTime? endTime = null,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilterForDoctorWorkList(
                await WithDetailsAsync(e => e.Doctor), userId, status, startTime, endTime
            )
            .LongCountAsync(GetCancellationToken(cancellationToken));

#region ApplyFilter

    protected virtual IQueryable<Protocol> ApplyFilter(
        IQueryable<Protocol> query,
        string? filterText = null, // todo
        Guid? patientId = null,
        Guid? doctorId = null,
        Guid? departmentId = null,
        Guid? protocolTypeId = null,
        Guid? protocolTypeActionId = null,
        EnumProtocolStatus status = EnumProtocolStatus.None,
        DateTime? startTime = null,
        DateTime? endTime = null
    ) =>
        query
            .WhereIf(patientId.HasValue, e => e.PatientId == patientId!.Value)
            .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId!.Value)
            .WhereIf(departmentId.HasValue, e => e.DepartmentId == departmentId!.Value)
            .WhereIf(protocolTypeId.HasValue, e => e.ProtocolTypeId == protocolTypeId!.Value)
            .WhereIf(protocolTypeActionId.HasValue, e => e.ProtocolTypeActionId == protocolTypeActionId!.Value)
            .WhereIf(status != EnumProtocolStatus.None, e => e.Status == status)
            .WhereIf(startTime.HasValue, e => e.StartTime >= startTime!.Value)
            .WhereIf(endTime.HasValue, e => !e.EndTime.HasValue || e.EndTime <= endTime!.Value);

    protected virtual IQueryable<Protocol> ApplyFilterForDoctorWorkList(
        IQueryable<Protocol> query,
        Guid userId,
        ICollection<EnumProtocolStatus>? status = null,
        DateTime? startTime = null,
        DateTime? endTime = null
    ) =>
        query
            .Where(e => e.Doctor.UserId == userId)
            .WhereIf(status != null && status.Count != 0, e => status!.Contains(e.Status))
            .WhereIf(startTime.HasValue, e => e.StartTime >= startTime!.Value)
            .WhereIf(endTime.HasValue, e => !e.EndTime.HasValue || e.EndTime <= endTime!.Value);

#endregion

    protected virtual string GetSorting(string? sorting, bool withEntityName) =>
        sorting.IsNullOrWhiteSpace() ?
            ProtocolConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "Protocol." : string.Empty)}{sorting}";
}