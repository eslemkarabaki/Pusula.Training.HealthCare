using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Doctors;

public interface IDoctorRepository : IRepository<Doctor, Guid>
{
    Task<Doctor> GetAsync(
        Guid? doctorId,
        Guid? userId,
        CancellationToken cancellationToken = default
    );

    Task DeleteAllAsync(
        string? filterText = null,
        string? fullname = null,
        int? appointmentTime = null,
        Guid? titleId = null,
        Guid? departmentId = null,
        Guid? hospitalId = null,
        CancellationToken cancellationToken = default
    );

    Task<List<Doctor>> GetListAsync(
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
    );

    Task<List<DoctorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
    );

    Task<long> GetCountAsync(
        string? filterText = null,
        string? fullname = null,
        int? appointmentTime = null,
        Guid? titleId = null,
        Guid? departmentId = null,
        Guid? hospitalId = null,
        CancellationToken cancellationToken = default
    );
}