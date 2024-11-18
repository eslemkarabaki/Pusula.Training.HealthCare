using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Addresses;

public sealed class Address : AuditedEntity<Guid>, IAddress
{
    public Guid PatientId { get; private set; }
    public Guid DistrictId { get; private set; }
    public string AddressTitle { get; private set; }
    public string AddressLine { get; private set; }


    private Address()
    {
        AddressTitle = string.Empty;
        AddressLine = string.Empty;
    }


    internal Address(Guid id, Guid patientId, Guid districtId, string addressTitle, string addressLine) : base(id)
    {
        Set(patientId, districtId, addressTitle, addressLine);
    }

    internal void Set(Guid patientId, Guid districtId, string addressTitle, string addressLine)
    {
        PatientId = Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
        DistrictId = Check.NotDefaultOrNull<Guid>(districtId, nameof(districtId));
        AddressTitle = Check.NotNullOrWhiteSpace(addressTitle, nameof(addressTitle), AddressConsts.TitleMaxLength);
        AddressLine = Check.NotNullOrWhiteSpace(addressLine, nameof(addressLine), AddressConsts.AddressMaxLength);
    }
}