using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Countries;

public class Country : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }

    protected Country()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    public Country(string name, string code)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name), CountryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(code, nameof(code), CountryConsts.CodeMaxLength);

        Name = name;
        Code = code;
    }
}