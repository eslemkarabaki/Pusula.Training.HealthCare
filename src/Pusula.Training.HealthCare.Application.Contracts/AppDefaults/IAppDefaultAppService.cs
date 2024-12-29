using System;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.AppDefaults;

public interface IAppDefaultAppService : IApplicationService
{
    Task<CountryDto> GetCurrentCountryAsync();
    Task SetCurrentCountryAsync(Guid countryId);
}