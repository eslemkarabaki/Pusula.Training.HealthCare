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

    protected Country()
    {
        Name = string.Empty;
        Iso = string.Empty;
        PhoneCode = string.Empty;
    }

    public Country(
        Guid id,
        string name,
        string iso,
        string phoneCode,
        bool isCurrent = false
    ) : base(id)
    {
        SetIsCurrent(isCurrent);
        SetName(name);
        SetIso(iso);
        SetPhoneCode(phoneCode);
    }

    public void SetIsCurrent(bool isCurrent) => IsCurrent = isCurrent;

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CountryConsts.NameMaxLength);

    public void SetIso(string iso) =>
        Iso =
            Check.NotNullOrWhiteSpace(iso, nameof(iso), CountryConsts.IsoMaxLength);

    public void SetPhoneCode(string phoneCode) =>
        PhoneCode = Check.NotNullOrWhiteSpace(phoneCode, nameof(phoneCode), CountryConsts.PhoneCodeMaxLength);
}