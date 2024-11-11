using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Cities;

public class CityManager(ICityRepository cityRepository) : DomainService
{
    public virtual async Task<City> CreateAsync(
        Guid countryId,
        string name
    )
    {
        Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));
        Check.NotNullOrWhiteSpace(name, nameof(name), CityConsts.NameMaxLength);

        var city = new City(GuidGenerator.Create(), countryId, name);
        return await cityRepository.InsertAsync(city);
    }

    public virtual async Task<City> UpdateAsync(
        Guid id,
        Guid countryId,
        string name,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));
        Check.NotNullOrWhiteSpace(name, nameof(name), CityConsts.NameMaxLength);

        var city = await cityRepository.GetAsync(id);

        city.Name = name;
        city.CountryId = countryId;

        city.SetConcurrencyStampIfNotNull(concurrencyStamp);

        return await cityRepository.UpdateAsync(city);
    }
}