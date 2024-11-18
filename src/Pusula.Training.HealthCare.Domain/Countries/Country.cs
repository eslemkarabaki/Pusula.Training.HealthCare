using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Countries;

public sealed class Country : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string Iso { get; private set; }
    public string PhoneCode { get; private set; }
    public bool IsCurrent { get; private set; }

    private Country()
    {
        Name = string.Empty;
        Iso = string.Empty;
        PhoneCode = string.Empty;
    }

    internal Country(Guid id, string name, string iso, string phoneCode, bool isCurrent = false) : base(id)
    {
        Set(name, iso, phoneCode, isCurrent);
    }

    internal void Set(string name, string iso, string phoneCode, bool isCurrent = false)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CountryConsts.NameMaxLength);
        Iso =
            Check.NotNullOrWhiteSpace(iso, nameof(iso), CountryConsts.IsoMaxLength);
        PhoneCode = Check.NotNullOrWhiteSpace(phoneCode, nameof(phoneCode), CountryConsts.PhoneCodeMaxLength);
        IsCurrent = isCurrent;
    }
}