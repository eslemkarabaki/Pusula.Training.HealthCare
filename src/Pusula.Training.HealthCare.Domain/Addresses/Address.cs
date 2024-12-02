using System;
using Pusula.Training.HealthCare.Districts;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Addresses;

public sealed class Address : AuditedEntity<Guid>, IAddress
{
    public Guid PatientId { get; private set; }
    public Guid DistrictId { get; private set; }
    public District District { get; set; }
    public string AddressTitle { get; private set; }
    public string AddressLine { get; private set; }

    protected Address()
    {
        AddressTitle = string.Empty;
        AddressLine = string.Empty;
    }

    public Address(
        Guid id,
        Guid patientId,
        Guid districtId,
        string addressTitle,
        string addressLine
    ) : base(id)
    {
        SetPatientId(patientId);
        SetDistrictId(districtId);
        SetAddressTitle(addressTitle);
        SetAddressLine(addressLine);
    }

    public void SetPatientId(Guid patientId) => PatientId = Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));

    public void SetDistrictId(Guid districtId) =>
        DistrictId = Check.NotDefaultOrNull<Guid>(districtId, nameof(districtId));

    public void SetAddressTitle(string addressTitle) =>
        AddressTitle = Check.NotNullOrWhiteSpace(addressTitle, nameof(addressTitle), AddressConsts.TitleMaxLength);

    public void SetAddressLine(string addressLine) =>
        AddressLine = Check.NotNullOrWhiteSpace(addressLine, nameof(addressLine), AddressConsts.AddressMaxLength);
}