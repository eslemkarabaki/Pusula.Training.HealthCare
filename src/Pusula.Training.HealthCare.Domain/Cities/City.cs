using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Cities;

public sealed class City : FullAuditedAggregateRoot<Guid>
{
    public Guid CountryId { get; set; }
    public string Name { get; set; }

    private City()
    {
        Name = string.Empty;
    }


    public City(Guid id, Guid countryId, string name)
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));
        Check.NotNullOrWhiteSpace(name, nameof(name), CityConsts.NameMaxLength);

        Id = id;
        CountryId = countryId;
        Name = name;
    }
}