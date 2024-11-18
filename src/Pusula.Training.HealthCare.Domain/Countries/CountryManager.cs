using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Countries;

public class CountryManager(ICountryRepository countryRepository) : DomainService
{
    public virtual async Task<Country> CreateAsync(
        string name,
        string iso,
        string phoneCode,
        bool isCurrent = false
    )
    {
        var country = new Country(GuidGenerator.Create(), name, iso, phoneCode, isCurrent);
        return await countryRepository.InsertAsync(country);
    }

    public virtual async Task<Country> UpdateAsync(
        Guid id,
        string name,
        string iso,
        string phoneCode,
        bool isCurrent = false,
        string? concurrencyStamp = null
    )
    {
        var country = await countryRepository.GetAsync(id);
        country.Set(name, iso, phoneCode, isCurrent);
        country.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await countryRepository.UpdateAsync(country);
    }
}