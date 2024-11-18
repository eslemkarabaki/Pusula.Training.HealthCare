using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Addresses;

public interface IAddressRepository : IRepository<Address, Guid>
{
    Task<List<AddressView>> GetViewListAsync(
        Guid? patientId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );
}