using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Cities;

public sealed class City : FullAuditedAggregateRoot<Guid>
{
    public Guid CountryId { get; private set; }
    public string Name { get; private set; }

    private City()
    {
        Name = string.Empty;
    }


    internal City(Guid id, Guid countryId, string name) : base(id)
    {
        Set(countryId, name);
    }

    internal void Set(Guid countryId, string name)
    {
        CountryId = Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CityConsts.NameMaxLength);
    }
}