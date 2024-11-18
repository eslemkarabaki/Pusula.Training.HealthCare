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
        var city = new City(GuidGenerator.Create(), countryId, name);
        return await cityRepository.InsertAsync(city);
    }

    public virtual async Task<City> UpdateAsync(
        Guid id,
        Guid countryId,
        string name,
        string? concurrencyStamp = null
    )
    {
        var city = await cityRepository.GetAsync(id);
        city.Set(countryId, name);
        city.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await cityRepository.UpdateAsync(city);
    }
}