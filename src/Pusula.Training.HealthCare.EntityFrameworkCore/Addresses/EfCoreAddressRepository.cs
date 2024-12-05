using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.Addresses;

public class EfCoreAddressRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, Address, Guid>(dbContextProvider), IAddressRepository
{
    public async Task<List<Address>> GetListAsync(
        Guid? patientId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(await GetQueryableAsync(), patientId)
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    public async Task<List<Address>> GetListWithDetailsAsync(
        Guid? patientId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    ) =>
        await ApplyFilter(
                  await WithDetailsAsync(e => e.District.City.Country), patientId
              )
              .OrderBy(GetSorting(sorting, false))
              .PageBy(skipCount, maxResultCount)
              .ToListAsync(GetCancellationToken(cancellationToken));

    protected virtual IQueryable<Address> ApplyFilter(
        IQueryable<Address> query,
        Guid? patientId = null
    ) =>
        query
            .WhereIf(patientId.HasValue, e => e.PatientId == patientId!.Value);

    private string GetSorting(
        string? sorting,
        bool withEntityName
    ) =>
        sorting.IsNullOrWhiteSpace() ?
            AddressConsts.GetDefaultSorting(withEntityName) :
            $"{(withEntityName ? "Address" : string.Empty)}{sorting}";
}