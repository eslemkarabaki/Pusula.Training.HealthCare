using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Cities;

public interface ICityRepository : IRepository<City, Guid>
{
    Task<List<CityWithCountry>> GetListAsync(
        string? filterText = null,
        string? name = null,
        Guid? countryId = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string? filterText = null,
        string? name = null,
        Guid? countryId = null,
        CancellationToken cancellationToken = default);

    Task DeleteAllAsync(
        string? filterText = null,
        string? name = null,
        Guid? countryId = null,
        CancellationToken cancellationToken = default);
}