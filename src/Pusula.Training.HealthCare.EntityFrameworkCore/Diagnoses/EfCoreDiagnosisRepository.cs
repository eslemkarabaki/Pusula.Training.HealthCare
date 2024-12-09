using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Diagnoses;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Diagnoses
{
    public class EfCoreDiagnosisRepository : EfCoreRepository<HealthCareDbContext, Diagnosis, Guid>, IDiagnosisRepository
    {
        public EfCoreDiagnosisRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public async Task<Diagnosis> FindByCodeAsync(string code)
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Diagnoses.FirstOrDefaultAsync(d => d.Code == code);
        }
        public async Task<Diagnosis> FindByNameAsync(string name)
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Diagnoses.FirstOrDefaultAsync(d => d.Name == name);
        }
        public async Task<List<Diagnosis>> GetAllAsync()
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Diagnoses.ToListAsync();
        }
        public async Task<long> GetCountAsync(
            string? name = null,
            CancellationToken cancellationToken = default
        ) =>
            await ApplyFilter(await GetQueryableAsync(), name)
                .LongCountAsync(GetCancellationToken(cancellationToken));
        public async Task<List<Diagnosis>> GetListAsync(
            string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        ) =>
            await ApplyFilter(await GetQueryableAsync(), name)
                .OrderBy(GetSorting(sorting, false))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        protected virtual IQueryable<Diagnosis> ApplyFilter(
            IQueryable<Diagnosis> query,
            string? name = null
        ) =>
            query
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!));
        protected virtual string GetSorting(string? sorting, bool withEntityName)
        {
            if (string.IsNullOrWhiteSpace(sorting))
            {
                return string.Empty;
            }
            return $"{(withEntityName ? "Diagnosis." : string.Empty)}{sorting}";
        }
    }
}
