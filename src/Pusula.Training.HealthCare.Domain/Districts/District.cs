using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Districts;

public sealed class District : FullAuditedAggregateRoot<Guid>
{
    public Guid CityId { get; private set; }
    public string Name { get; private set; }


    private District()
    {
        Name = string.Empty;
    }

    public District(Guid id, Guid cityId, string name) : base(id)
    {
        Set(cityId, name);
    }

    internal void Set(Guid cityId, string name)
    {
        CityId = Check.NotDefaultOrNull<Guid>(cityId, nameof(cityId));
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), DistrictConsts.NameMaxLength);
    }
}