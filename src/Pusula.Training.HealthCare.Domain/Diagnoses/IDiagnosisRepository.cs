using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Diagnoses;

public interface IDiagnosisRepository : IRepository<Diagnosis, Guid>
{
    Task<Diagnosis> FindByCodeAsync(string code);
}
