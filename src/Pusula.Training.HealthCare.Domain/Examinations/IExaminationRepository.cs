using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
namespace Pusula.Training.HealthCare.Examinations;
public interface IExaminationRepository : IRepository<Examination, Guid>
{
    Task<ExaminationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id);

    Task DeleteAllAsync(
    CancellationToken cancellationToken = default);
    Task<List<ExaminationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
     Guid? ProtocolId = null,
     Guid? DoctorId = null  ,
     Guid? PatientId = null ,
     DateTime? StartDate = null,
     string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(
        Guid? ProtocolId = null,
        Guid? DoctorId = null,
        Guid? PatientId = null,
        DateTime? StartDate = null,
        CancellationToken cancellationToken = default);
}
