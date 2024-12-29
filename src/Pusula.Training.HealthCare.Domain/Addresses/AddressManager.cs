using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressManager(IAddressRepository addressRepository) : DomainService
{
    public async Task CreateAddressesAsync(Guid patientId, IEnumerable<Address> addresses)
    {
        if (addresses.Any())
        {
            var addressList = addresses.Select(
                e => new Address(GuidGenerator.Create(), patientId, e.DistrictId, e.AddressTitle, e.AddressLine)
            );
            await addressRepository.InsertManyAsync(addressList);
        }
    }

    private async Task CreateAddressesAsync(
        Guid patientId,
        IEnumerable<Address> existingAddresses,
        IEnumerable<Address> addresses
    )
    {
        var created = addresses
            .Where(e => !existingAddresses.Any(a => a.Id == e.Id));

        if (created.Any())
        {
            var addressList = created.Select(
                e => new Address(GuidGenerator.Create(), patientId, e.DistrictId, e.AddressTitle, e.AddressLine)
            );
            await addressRepository.InsertManyAsync(addressList);
        }
    }

    private async Task UpdateAddressesAsync(IEnumerable<Address> existingAddresses, IEnumerable<Address> addresses)
    {
        var updated = addresses
            .Where(e => existingAddresses.Any(a => a.Id == e.Id));

        if (updated.Any())
        {
            var entities = existingAddresses.Where(e => updated.Any(u => u.Id == e.Id));
            foreach (var entity in entities)
            {
                var address = addresses.First(e => e.Id == entity.Id);
                entity.SetDistrictId(address.DistrictId);
                entity.SetAddressTitle(address.AddressTitle);
                entity.SetAddressLine(address.AddressLine);
            }

            await addressRepository.UpdateManyAsync(entities);
        }
    }

    private async Task DeleteAddressesAsync(IEnumerable<Address> existingAddresses, IEnumerable<Address> addresses)
    {
        var deleted = existingAddresses
                      .Where(e => !addresses.Any(a => a.Id == e.Id))
                      .Select(e => e.Id);
        if (deleted.Any())
        {
            await addressRepository.DeleteManyAsync(deleted);
        }
    }

    public async Task SetAddressesAsync(Guid patientId, IEnumerable<Address> addresses)
    {
        var existingAddresses = await addressRepository.GetListAsync(e => e.PatientId == patientId);
        if (existingAddresses.Count == 0)
        {
            await CreateAddressesAsync(patientId, addresses);
            return;
        }

        if (!addresses.Any())
        {
            await addressRepository.DeleteAsync(e => true);
            return;
        }

        await DeleteAddressesAsync(existingAddresses, addresses);
        await UpdateAddressesAsync(existingAddresses, addresses);
        await CreateAddressesAsync(patientId, existingAddresses, addresses);
    }
}