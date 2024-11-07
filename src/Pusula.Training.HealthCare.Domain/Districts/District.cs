using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Districts;

public sealed class District : FullAuditedAggregateRoot<Guid>
{
    public Guid CityId { get; set; }
    public string Name { get; set; }


    private District()
    {
        Name = string.Empty;
    }

    public District(Guid id, Guid cityId, string name)
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotDefaultOrNull<Guid>(cityId, nameof(cityId));
        Check.NotNullOrWhiteSpace(name, nameof(name), DistrictConsts.NameMaxLength);

        Id = id;
        CityId = cityId;
        Name = name;
    }
}