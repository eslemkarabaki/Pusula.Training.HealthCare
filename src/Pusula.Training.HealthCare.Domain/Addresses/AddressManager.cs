using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressManager(IAddressRepository addressRepository) : DomainService
{
    public async Task CreateAddressAsync(Guid patientId, IEnumerable<Address> addresses) =>
        await addressRepository.InsertManyAsync(addresses);

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

    public async Task UpdateAddressAsync(Guid patientId, Address address)
    {
        var entity = await addressRepository.GetAsync(address.Id);
        entity.Set(patientId, address.DistrictId, address.AddressTitle, address.AddressLine);
        await addressRepository.UpdateAsync(entity);
    }

    public async Task DeleteAddressAsync(Guid id) => await addressRepository.DeleteAsync(id);

    public async Task UpdateOrCreateAddressAsync(Guid patientId, IEnumerable<Address> addresses)
    {
        var entities = await addressRepository.GetListAsync(e => e.PatientId == patientId);

        // delete
        foreach (var address in entities.Where(e => !addresses.Any(a => a.Id == e.Id)))
        {
            await DeleteAddressAsync(address.Id);
        }

        // update
        foreach (var address in addresses.Where(e => entities.Any(a => a.Id == e.Id)))
        {
            await UpdateAddressAsync(patientId, address);
        }

        // create
        foreach (var address in addresses.Where(e => !entities.Any(a => a.Id == e.Id)))
        {
            await CreateAddressAsync(patientId, address);
        }
    }
}