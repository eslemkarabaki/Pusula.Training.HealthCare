using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Hospitals
{
    public class EfCoreHospitalRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Hospital, Guid>(dbContextProvider), IHospitalRepository
    {
        public async Task DeleteAllAsync(
            string? filterText = null, 
            string? name = null, string? 
            address = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, name, address);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public async Task<HospitalWithDepartment> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await ApplyFilterAsync();
            return await query.Where(x => x.Id == id).FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }
  
        public async Task<long> GetCountAsync(
            string? filterText = null, 
            string? name = null,
            string? address = null, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, address);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<List<Hospital>> GetListAsync(
            string? filterText = null, 
            string? name = null, 
            string? address = null, 
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0, 
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, address);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? HospitalConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<List<HospitalWithDepartment>> GetListAsync(string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
        {
            var query = await ApplyFilterAsync();
            return await query
                .OrderBy(string.IsNullOrWhiteSpace(sorting) ? HospitalConsts.GetDefaultSorting(true) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Hospital> ApplyFilter(
            IQueryable<Hospital> query,
            string? filterText = null,
            string? name = null,
            string? address = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Address!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
                    .WhereIf(!string.IsNullOrWhiteSpace(address), e => e.Address.Contains(address!));
                  
        }

        protected virtual async Task<IQueryable<HospitalWithDepartment>> ApplyFilterAsync()
        {
            var dbContext = await GetDbContextAsync();
            return (await GetDbSetAsync())
                .Include(x => x.HospitalDepartments)
                .Select(h => new
                {
                    hospital = h,
                    departmentNames = (from hd in h.HospitalDepartments 
                                       join d in dbContext.Set<Department>() on hd.DepartmentId equals d.Id
                                       select d.Name
                                       )
                    .Distinct()
                    .ToArray()

                })
                .Select(x => new HospitalWithDepartment
                {
                    Id = x.hospital.Id,
                    Name = x.hospital.Name,
                    Address = x.hospital.Address,
                    CreationTime = x.hospital.CreationTime,
                    DepartmentNames = x.departmentNames
                });

        }  

        //protected virtual async Task<IQueryable<HospitalWithDepartment>> ApplyFilterAsync()
        //{
        //    var dbContext = await GetDbContextAsync();
        //    return (await GetDbSetAsync())
        //        .Include(x => x.HospitalDepartments)
        //        .Join(dbContext.Set<Department>(), hospital => default, department => department.Id, (hospital, department) => new { hospital, department })
        //        .Select(x => new HospitalWithDepartment
        //        {
        //            Id = x.hospital.Id,
        //            Name = x.hospital.Name,
        //            Address = x.hospital.Address,
        //            CreationTime = x.hospital.CreationTime,
        //            DepartmentNames = (from hd in x.hospital.HospitalDepartments
        //                               join d in dbContext.Set<Department>() on hd.DepartmentId equals d.Id
        //                               select d.Name).ToArray() 
        //        });

        //}
         
    }
}
