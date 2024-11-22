using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressManager(IAddressRepository addressRepository) : DomainService
{
    public async Task CreateAddressAsync(Guid patientId, Address address) =>
        await addressRepository.InsertAsync(
            new Address(
                GuidGenerator.Create(),
                patientId,
                address.DistrictId,
                address.AddressTitle,
                address.AddressLine
            )
        );

    public async Task CreateAddressAsync(Guid patientId, IEnumerable<Address> addresses)
    {
        foreach (var address in addresses)
        {
            await CreateAddressAsync(patientId, address);
        }
    }

    public async Task UpdateAddressAsync(Address address)
    {
        var entity = await addressRepository.GetAsync(address.Id);
        entity.SetDistrictId(address.DistrictId);
        entity.SetAddressTitle(address.AddressTitle);
        entity.SetAddressLine(address.AddressLine);
        await addressRepository.UpdateAsync(entity);
    }

    public async Task UpdateOrCreateAddressAsync(Guid patientId, IEnumerable<Address> addresses)
    {
        var existingAddresses = await addressRepository.GetListAsync(e => e.PatientId == patientId);

        // delete
        var addressesToDelete = existingAddresses
                                .Where(e => !addresses.Any(a => a.Id == e.Id))
                                .Select(e => e.Id);

        // update
        var addressesToUpdate = addresses
            .Where(e => existingAddresses.Any(a => a.Id == e.Id));

        // create
        var addressesToCreate = addresses
            .Where(e => !existingAddresses.Any(a => a.Id == e.Id));

        await Task.WhenAll(addressesToDelete.Select(e => addressRepository.DeleteAsync(e)));
        await Task.WhenAll(addressesToUpdate.Select(UpdateAddressAsync));
        await CreateAddressAsync(patientId, addressesToCreate);

        // var entities = await addressRepository.GetListAsync(e => e.PatientId == patientId);
        //
        // // delete
        // foreach (var address in entities.Where(e => !addresses.Any(a => a.Id == e.Id)))
        // {
        //     await addressRepository.DeleteAsync(address.Id);
        // }
        //
        // // update
        // foreach (var address in addresses.Where(e => entities.Any(a => a.Id == e.Id)))
        // {
        //     await UpdateAddressAsync(address);
        // }
        //
        // // create
        // await CreateAddressAsync(patientId, addresses.Where(e => !entities.Any(a => a.Id == e.Id)));
    }
}