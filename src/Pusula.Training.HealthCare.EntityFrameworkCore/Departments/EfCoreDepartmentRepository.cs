using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Pusula.Training.HealthCare.Hospitals;

namespace Pusula.Training.HealthCare.Departments;

public class EfCoreDepartmentRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider) 
    : EfCoreRepository<HealthCareDbContext, Department, Guid>(dbContextProvider), IDepartmentRepository
{
    public virtual async Task DeleteAllAsync(
        string? filterText = null,
                    string? name = null,
                    string? description = null,
                    int? duration = null,
        CancellationToken cancellationToken = default)
    {

        var query = await GetQueryableAsync();

        query = ApplyFilter(query, filterText, name, description, duration);

        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public async Task<DepartmentWithHospital> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = await ApplyFilterAsync();
        return await query.Where(x => x.Id == id).FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Department>> GetListAsync(
        string? filterText = null,
        string? name = null,
        string? description = null,
        int? duration = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name, description, duration);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DepartmentConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public async Task<List<DepartmentWithHospital>> GetListAsync(string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await ApplyFilterAsync();
        return await query
            .OrderBy(string.IsNullOrWhiteSpace(sorting) ? DepartmentConsts.GetDefaultSorting(true) : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
        string? filterText = null,
        string? name = null,
        string? description = null,
        int? duration = null,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name, description, duration);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    } 

    protected virtual IQueryable<Department> ApplyFilter(
        IQueryable<Department> query,
        string? filterText = null,
        string? name = null,
        string? description = null,
        int? duration = null)
    {
        return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name!))
                .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description!))
                .WhereIf(duration.HasValue, e => e.Duration == duration);
    }
     
    protected virtual async Task<IQueryable<DepartmentWithHospital>> ApplyFilterAsync()
    {
        var dbContex = await GetDbContextAsync();
        return (await GetDbSetAsync())
            .Include(x => x.HospitalDepartments)
            .Select(d => new
            {
                department = d,
                hospitalNames = (from hd in d.HospitalDepartments
                                 join h in dbContex.Set<Hospital>() on hd.HospitalId equals h.Id
                                 select h.Name)

                .Distinct()
                .ToArray()
            })
            .Select(x => new DepartmentWithHospital
            {
                Id = x.department.Id,
                Name = x.department.Name,
                Description = x.department.Description,
                Duration = x.department.Duration,
                CreationTime = x.department.CreationTime,
                HospitalNames = x.hospitalNames
            });
    }

}