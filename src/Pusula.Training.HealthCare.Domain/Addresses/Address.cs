using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Addresses;

public sealed class Address : FullAuditedAggregateRoot<Guid>
{
    public Guid PatientId { get; set; }
    public Guid DistrictId { get; set; }
    public string AddressLine { get; set; }


    private Address()
    {
        AddressLine = string.Empty;
    }


    public Address(Guid id, Guid patientId, Guid districtId, string addressLine)
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
        Check.NotDefaultOrNull<Guid>(districtId, nameof(districtId));
        Check.NotNullOrWhiteSpace(addressLine, nameof(addressLine));

        Id = id;
        PatientId = patientId;
        DistrictId = districtId;
        AddressLine = addressLine;
    }
}