// EF Core Repository for ExaminationDiagnosis
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.EntityFrameworkCore.ExaminationDiagnoses
{
    public class EfCoreExaminationDiagnosisRepository : EfCoreRepository<HealthCareDbContext, ExaminationDiagnosis, Guid>, IExaminationDiagnosisRepository
    {
        public EfCoreExaminationDiagnosisRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<ExaminationDiagnosis>> GetListAsync(Guid? examinationId, Guid? diagnosisId, string explanation, string type)
        {
            var query = await GetDbSetAsync();
            return await query  
                .WhereIf(examinationId.HasValue, x => x.ExaminationId == examinationId.Value)
                .WhereIf(diagnosisId.HasValue, x => x.DiagnosisId == diagnosisId.Value)
                .WhereIf(!string.IsNullOrWhiteSpace(explanation), x => x.Explanation.Contains(explanation))
                .WhereIf(!string.IsNullOrWhiteSpace(type), x => x.Type.Contains(type))
                .ToListAsync();
        }
    }
}
