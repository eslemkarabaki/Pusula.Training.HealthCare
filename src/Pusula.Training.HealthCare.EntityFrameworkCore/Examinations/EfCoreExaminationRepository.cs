using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Examinations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Pusula.Training.HealthCare.Protocols;


namespace Pusula.Training.HealthCare.Examinations
{
    public class EfCoreExaminationRepository : EfCoreRepository<HealthCareDbContext, Examination, Guid>, IExaminationRepository
    {
        private readonly IProtocolRepository _protocolRepository;
        public EfCoreExaminationRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider,IProtocolRepository protocolRepository)
            : base(dbContextProvider)
        {
            _protocolRepository = protocolRepository;
        }
        
        // Delete all records
        public virtual async Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();
            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        // Get count of records with optional filters
        public virtual async Task<long> GetCountAsync(
           Guid? protocolId = null, Guid? doctorId = null, Guid? patientId = null, DateTime? startDate = null, CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()),protocolId, doctorId, patientId, startDate);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        // Helper method to apply filters to the query
        protected virtual IQueryable<Examination> ApplyFilter(
            IQueryable<Examination> query,
             Guid? protocolId = null,
            Guid? doctorId = null,
            Guid? patientId = null,
            DateTime? startDate = null)
        {
            return query
                .WhereIf(protocolId.HasValue, e => e.ProtocolId == protocolId.Value)
                .WhereIf(doctorId.HasValue, e => e.DoctorId == doctorId.Value)
                .WhereIf(patientId.HasValue, e => e.PatientId == patientId.Value)
                .WhereIf(startDate.HasValue, e => e.StartDate.Date == startDate.Value.Date);
        }

        public async Task<ExaminationWithNavigationProperties> GetWithNavigationPropertiesAsync(int protocolNo)
        {

           var protocol = await _protocolRepository.GetAsync(e => e.ProtocolNo == protocolNo);
         return await (await GetQuaryableForNavigationProperties()).FirstOrDefaultAsync(e => e.Examination.ProtocolId == protocol.Id);
            
        }
        public async Task<List<ExaminationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(Guid? protocolId = null,
     Guid? doctorId = null,
     Guid? patientId = null,
     DateTime? startDate = null,
     string? sorting = null,
        int maxResultCount = int.MaxValue,  
        int skipCount = 0,
        CancellationToken cancellationToken = default)
        {

         return  await  ApplyFilter ((await GetQuaryableForNavigationProperties()), protocolId, doctorId, patientId, startDate)
            .OrderBy(string.IsNullOrEmpty(sorting) ? ExaminationConsts.GetDefaultSorting(true) : sorting)
                .PageBy(skipCount, maxResultCount).ToListAsync(GetCancellationToken(cancellationToken));

        }
        protected virtual async Task<IQueryable<ExaminationWithNavigationProperties>> GetQuaryableForNavigationProperties()
        {
            var dbContext = await GetDbContextAsync();
                
            return from examination in dbContext.Examinations
                        join examinationAnamnez in dbContext.ExaminationAnamnez
                        on examination.Id equals examinationAnamnez.ExaminationId
                        into examinationAnamnezs
                   from examinationAnamnez in examinationAnamnezs.DefaultIfEmpty()
                   join examinationDiagnosis in dbContext.ExaminationDiagnoses
                        on examination.Id equals examinationDiagnosis.ExaminationId
                        into examinationDiagnosiss
                   from examinationDiagnosis in examinationDiagnosiss.DefaultIfEmpty()
                   join examinationPhysical in dbContext.ExaminationPhysical
                        on examination.Id equals examinationPhysical.ExaminationId
                        into examinationPhysicals
                   from examinationPhysical in examinationPhysicals.DefaultIfEmpty()
                   select new ExaminationWithNavigationProperties
                        {
                          Examination = examination,
                          ExaminationAnamnez = examinationAnamnez,
                          ExaminationDiagnoses = examinationDiagnosis,
                          ExaminationPhysical = examinationPhysical
                        };
        }

        protected virtual async Task<IQueryable<ExaminationWithNavigationProperties>> GetQueryableByProtocolNo()
        {
            var dbContext = await GetDbContextAsync();

            var query = dbContext.Examinations
                .Include(e => e.ExaminationAnamnez) // Include related entities without filtering
                .Include(e => e.ExaminationDiagnoses)
                .Include(e => e.ExaminationPhysical)
                .Select(examination => new ExaminationWithNavigationProperties
                {
                    Examination = examination,
                    ExaminationAnamnez = examination.ExaminationAnamnez, // Navigation property
                    ExaminationDiagnoses = examination.ExaminationDiagnoses,
                    ExaminationPhysical = examination.ExaminationPhysical
                });

            return query;
        }
        protected virtual IQueryable<ExaminationWithNavigationProperties> ApplyFilter(
           IQueryable<ExaminationWithNavigationProperties> query,
            Guid? protocolId = null,
           Guid? doctorId = null,
           Guid? patientId = null,
           DateTime? startDate = null)
        {
            return query
                .WhereIf(protocolId.HasValue, e => e.Examination.ProtocolId == protocolId.Value)
                .WhereIf(doctorId.HasValue, e => e.Examination.DoctorId == doctorId.Value)
                .WhereIf(patientId.HasValue, e => e.Examination.PatientId == patientId.Value)
                .WhereIf(startDate.HasValue, e => e.Examination.StartDate.Date == startDate.Value.Date);
        }

     
    }
}
