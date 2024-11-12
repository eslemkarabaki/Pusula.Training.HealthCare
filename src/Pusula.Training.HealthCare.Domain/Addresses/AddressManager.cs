using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Patients;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressManager(IAddressRepository addressRepository) : DomainService
{
    public virtual async Task<Address> CreateAsync(Guid patientId, Guid districtId, string addressLine)
    {
        Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
        Check.NotDefaultOrNull<Guid>(districtId, nameof(districtId));
        Check.NotNullOrWhiteSpace(addressLine, nameof(addressLine));

        var address = new Address(GuidGenerator.Create(), patientId, districtId, addressLine);
        return await addressRepository.InsertAsync(address);
    }

    public virtual async Task<Address> UpdateAsync(
        Guid id,
        Guid patientId,
        Guid districtId,
        string addressLine,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
        Check.NotDefaultOrNull<Guid>(districtId, nameof(districtId));
        Check.NotNullOrWhiteSpace(addressLine, nameof(addressLine));

        var address = await addressRepository.GetAsync(id);

        address.PatientId=patientId;
        address.DistrictId=districtId;
        address.AddressLine=addressLine;

        address.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await addressRepository.UpdateAsync(address);
    }
    
    public virtual async Task<Address> UpdateAsync(
        Guid patientId,
        Guid districtId,
        string addressLine,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
        Check.NotDefaultOrNull<Guid>(districtId, nameof(districtId));
        Check.NotNullOrWhiteSpace(addressLine, nameof(addressLine));

        var address = await addressRepository.GetAsync(e=>e.PatientId == patientId);

        address.PatientId=patientId;
        address.DistrictId=districtId;
        address.AddressLine=addressLine;

        address.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await addressRepository.UpdateAsync(address);
    }
}