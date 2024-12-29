using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses;

public interface IExaminationDiagnosisRepository : IRepository<ExaminationDiagnosis, Guid>
{
    //Task<List<ExaminationDiagnosis>> GetByExaminationIdAsync(Guid examinationId);
    Task<List<ExaminationDiagnosis>> GetListAsync(Guid? examinationId, Guid? diagnosisId, string explanation, string type);
}
