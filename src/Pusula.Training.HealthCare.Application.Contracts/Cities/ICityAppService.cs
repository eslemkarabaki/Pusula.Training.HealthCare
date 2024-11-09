using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Cities;

public interface ICityAppService : IApplicationService
{
    Task<IEnumerable<CityDto>> GetListAsync(Guid countryId);
}