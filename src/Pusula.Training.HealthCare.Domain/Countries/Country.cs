using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Countries;

public sealed class Country : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string Abbreviation { get; set; }

    private Country()
    {
        Name = string.Empty;
        Abbreviation = string.Empty;
    }

    public Country(Guid id, string name, string abbreviation)
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotNullOrWhiteSpace(name, nameof(name), CountryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(abbreviation, nameof(abbreviation), CountryConsts.AbbreviationMaxLength);

        Id = id;
        Name = name;
        Abbreviation = abbreviation;
    }
}