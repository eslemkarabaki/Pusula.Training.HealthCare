﻿using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Doctors
{
    public class EfCoreDoctorRepository : EfCoreRepository<HealthCareDbContext, Doctor, Guid>, IDoctorRepository
    {
        public EfCoreDoctorRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task DeleteAllAsync(
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? workingHours = null,
            Guid? titleId = null,
            Guid? departmentId = null,
            Guid? hospitalId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryableAsync();

            query = ApplyFilter(query, filterText, firstName, lastName, workingHours, titleId, departmentId, hospitalId);

            var ids = query.Select(x => x.Id);
            await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
        }

        public virtual async Task<List<Doctor>> GetListAsync(
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? workingHours = null,
            Guid? titleId = null,
            Guid? departmentId = null,
            Guid? hospitalId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, firstName, lastName, workingHours, titleId, departmentId, hospitalId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DoctorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? workingHours = null,
            Guid? titleId = null,
            Guid? departmentId = null,
            Guid? hospitalId = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, firstName, lastName, workingHours, titleId, departmentId, hospitalId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        private IQueryable<Doctor> ApplyFilter(
            IQueryable<Doctor> query,
            string? filterText = null,
            string? firstName = null,
            string? lastName = null,
            string? workingHours = null,
            Guid? titleId = null,
            Guid? departmentId = null,
            Guid? hospitalId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FirstName.Contains(filterText!) || e.LastName.Contains(filterText!) || e.TitleId.ToString().Contains(filterText!) || e.DepartmentId.ToString().Contains(filterText!) || e.HospitalId.ToString().Contains(filterText!))
                .WhereIf(!string.IsNullOrWhiteSpace(firstName), e => e.FirstName.Contains(firstName!))
                .WhereIf(!string.IsNullOrWhiteSpace(lastName), e => e.LastName.Contains(lastName!))
                .WhereIf(!string.IsNullOrWhiteSpace(workingHours), e => e.WorkingHours.Contains(workingHours!))
                .WhereIf(titleId.HasValue, e => e.TitleId == titleId.Value)
                .WhereIf(departmentId.HasValue, e => e.DepartmentId == departmentId.Value)
                .WhereIf(hospitalId.HasValue, e => e.HospitalId == hospitalId.Value);
        }

        public Task DeleteAllAsync(string? filterText, string? firstName, string? lastName, string? workingHours, int? titleId, int? departmentId, int? hospitalId)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync(string? filterText, string? firstName, string? lastName, string? workingHours, int? titleId, int? departmentId, int? hospitalId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Doctor>> GetListAsync(string? filterText, string? firstName, string? lastName, string? workingHours, int? titleId, int? departmentId, int? hospitalId, string? sorting, int maxResultCount, int skipCount)
        {
            throw new NotImplementedException();
        }
    }
}
