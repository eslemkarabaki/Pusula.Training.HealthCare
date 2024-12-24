using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.PatientHistories;

public interface IPatientHistoryRepository : IRepository<PatientHistory, Guid>
{
    Task<PatientHistory> GetAsync(
        Guid? patientHistoryId = null,
        Guid? patientId = null,
        CancellationToken cancellationToken = default
    );
}