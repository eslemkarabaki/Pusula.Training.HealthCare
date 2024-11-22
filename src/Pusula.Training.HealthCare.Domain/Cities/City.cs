using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Cities;

public sealed class City : FullAuditedAggregateRoot<Guid>
{
    public Guid CountryId { get; private set; }
    public string Name { get; private set; }

    protected City() => Name = string.Empty;

    public City(Guid id, Guid countryId, string name) : base(id)
    {
        SetCountryId(countryId);
        SetName(name);
    }

    public void SetCountryId(Guid countryId) => CountryId = Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));

    public void SetName(string name) => Name = Check.NotNullOrWhiteSpace(name, nameof(name), CityConsts.NameMaxLength);
}