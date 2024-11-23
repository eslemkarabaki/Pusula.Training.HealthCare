using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.AppDefaults;

public class AppDefaultManager(IAppDefaultRepository appDefaultRepository) : DomainService
{
    public async Task<AppDefault> SetCurrentCountry(Guid countryId)
    {
        var appDefault = await appDefaultRepository.FirstAsync();
        appDefault.SetCurrentCountryId(countryId);
        return await appDefaultRepository.UpdateAsync(appDefault);
    }
}