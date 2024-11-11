using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictManager(IDistrictRepository districtRepository) : DomainService
{
    public virtual async Task<District> CreateAsync(
        Guid cityId,
        string name
    )
    {
        Check.NotDefaultOrNull<Guid>(cityId, nameof(cityId));
        Check.NotNullOrWhiteSpace(name, nameof(name), DistrictConsts.NameMaxLength);

        var district = new District(GuidGenerator.Create(), cityId, name);
        return await districtRepository.InsertAsync(district);
    }

    public virtual async Task<District> UpdateAsync(
        Guid id,
        Guid cityId,
        string name,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotDefaultOrNull<Guid>(cityId, nameof(cityId));
        Check.NotNullOrWhiteSpace(name, nameof(name), DistrictConsts.NameMaxLength);

        var district = await districtRepository.GetAsync(id);

        district.Name = name;
        district.CityId = cityId;

        district.SetConcurrencyStampIfNotNull(concurrencyStamp);

        return await districtRepository.UpdateAsync(district);
    }
}