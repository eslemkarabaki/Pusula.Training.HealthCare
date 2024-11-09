using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Districts;

public interface IDistrictAppService : IApplicationService
{
    Task<IEnumerable<DistrictDto>> GetListAsync(Guid cityId);
}