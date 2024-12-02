using System;
using Pusula.Training.HealthCare.Cities;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Districts;

public sealed class District : FullAuditedAggregateRoot<Guid>
{
    public Guid CityId { get; private set; }
    public City City { get; set; }
    public string Name { get; private set; }

    private District() => Name = string.Empty;

    public District(Guid id, Guid cityId, string name) : base(id)
    {
        SetCityId(cityId);
        SetName(name);
    }

    public void SetCityId(Guid cityId) => CityId = Check.NotDefaultOrNull<Guid>(cityId, nameof(cityId));

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), DistrictConsts.NameMaxLength);
}