using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.PatientHistories;

public class EfCorePatientHistoryRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, PatientHistory, Guid>(dbContextProvider), IPatientHistoryRepository
{
    public async Task<PatientHistory> GetAsync(
        Guid? patientHistoryId = null,
        Guid? patientId = null,
        CancellationToken cancellationToken = default
    ) =>
        await (await GetQueryableAsync())
              .WhereIf(patientHistoryId.HasValue, e => e.Id == patientHistoryId)
              .WhereIf(patientId.HasValue, e => e.PatientId == patientId)
              .Include(e => e.Education)
              .Include(e => e.Job)
              .Include(e => e.Allergies).ThenInclude(e => e.Allergy)
              .Include(e => e.Medicines).ThenInclude(e => e.Medicine)
              .Include(e => e.Operations).ThenInclude(e => e.Operation)
              .Include(e => e.Vaccines).ThenInclude(e => e.Vaccine)
              .Include(e => e.BloodTransfusions).ThenInclude(e => e.BloodTransfusion)
              .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
}