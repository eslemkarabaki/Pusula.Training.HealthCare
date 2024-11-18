using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.PatientNotes;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace Pusula.Training.HealthCare.Patients;

public class PatientManager(
    IPatientRepository patientRepository,
    IPatientNoteRepository patientNoteRepository,
    IAddressRepository addressRepository) : DomainService
{
    public virtual async Task<Patient> CreateAsync(
        Guid countryId,
        Guid patientTypeId,
        string firstName,
        string lastName,
        DateTime birthDate,
        string? identityNumber,
        string? passportNumber,
        string emailAddress,
        string mobilePhoneNumberCode,
        string mobilePhoneNumber,
        string? homePhoneNumberCode,
        string? homePhoneNumber,
        EnumGender gender,
        EnumBloodType bloodType,
        EnumMaritalStatus maritalStatus,
        IEnumerable<Address> addresses)
    {
        await CheckIdentityAndPassportNumberAsync(identityNumber, passportNumber);

        var patient = new Patient(
            GuidGenerator.Create(), countryId, patientTypeId, firstName, lastName, birthDate, identityNumber,
            passportNumber, emailAddress,
            mobilePhoneNumberCode, mobilePhoneNumber, homePhoneNumberCode, homePhoneNumber, gender, bloodType,
            maritalStatus
        );
        await CreateAddressAsync(patient.Id, addresses);
        return await patientRepository.InsertAsync(patient);
    }

    public virtual async Task<Patient> UpdateAsync(
        Guid id,
        Guid countryId,
        Guid patientTypeId,
        string firstName,
        string lastName,
        DateTime birthDate,
        string? identityNumber,
        string? passportNumber,
        string emailAddress,
        string mobilePhoneNumberCode,
        string mobilePhoneNumber,
        string? homePhoneNumberCode,
        string? homePhoneNumber,
        EnumGender gender,
        EnumBloodType bloodType,
        EnumMaritalStatus maritalStatus,
        IEnumerable<Address> addresses,
        string? concurrencyStamp = null
    )
    {
        await CheckIdentityAndPassportNumberAsync(identityNumber, passportNumber, id);

        var patient = await patientRepository.GetAsync(id);
        patient.Set(countryId, patientTypeId, firstName, lastName, birthDate, identityNumber, passportNumber,
            emailAddress, mobilePhoneNumberCode, mobilePhoneNumber, homePhoneNumberCode, homePhoneNumber, gender,
            bloodType, maritalStatus);

        await UpdateOrCreateAddressAsync(patient.Id,addresses);
        patient.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await patientRepository.UpdateAsync(patient);
    }

    private async Task CreateAddressAsync(Guid patientId, IEnumerable<Address> addresses)
    {
        foreach (var address in addresses)
        {
            await CreateAddressAsync(patientId, address);
        }
    }
    
    private async Task CreateAddressAsync(Guid patientId, Address address)
    {
            await addressRepository.InsertAsync(new Address(
                GuidGenerator.Create(),
                patientId,
                address.DistrictId,
                address.AddressTitle,
                address.AddressLine
            ));
    }
    
    private async Task UpdateAddressAsync(Guid patientId, Address address)
    {
        
        var entity = await addressRepository.GetAsync(address.Id);
        entity.Set(patientId, address.DistrictId, address.AddressTitle, address.AddressLine);
        await addressRepository.UpdateAsync(entity);
    }
    
    private async Task DeleteAddressAsync(Guid id)
    {
        await addressRepository.DeleteAsync(id);
    }
    
    private async Task UpdateOrCreateAddressAsync(Guid patientId, IEnumerable<Address> addresses)
    {
        var entities= await addressRepository.GetListAsync(e=>e.PatientId == patientId);
        
        // delete
        foreach (var address in entities.Where(e=> !addresses.Any(a=>a.Id==e.Id)))
        {
            await DeleteAddressAsync(address.Id);
        }
        
        // update
        foreach (var address in addresses.Where(e=> entities.Any(a=>a.Id==e.Id)))
        {
            await UpdateAddressAsync(patientId, address);
        }
        
        // create
        foreach (var address in addresses.Where(e=> !entities.Any(a=>a.Id==e.Id)))
        {
            await CreateAddressAsync(patientId, address);
        }
    }

    public async Task<PatientNote> CreateNoteAsync(Guid patientId, string note)
    {
        return await patientNoteRepository.InsertAsync(new PatientNote(GuidGenerator.Create(), patientId, note));
    }

    public async Task<PatientNote> UpdateNoteAsync(Guid id, Guid patientId, string note)
    {
        var patientNote = await patientNoteRepository.GetAsync(id);
        patientNote.Set(patientId, note);
        return await patientNoteRepository.UpdateAsync(patientNote);
    }

    private async Task CheckIdentityAndPassportNumberAsync(string? identityNumber, string? passportNumber,
                                                           Guid? excludeId = null)
    {
        if (identityNumber.IsNullOrWhiteSpace() && passportNumber.IsNullOrWhiteSpace())
        {
            throw new Exception("Identity number or passport number is required.");
        }

        if (!identityNumber.IsNullOrWhiteSpace())
        {
            await CheckIdentityNumberNotExistAsync(excludeId, identityNumber);
        }
        else
        {
            await CheckPassportNumberNotExistAsync(excludeId, passportNumber!);
        }
    }

    private async Task CheckIdentityNumberNotExistAsync(Guid? excludeId, string identityNumber)
    {
        if (await patientRepository.IdentityNumberExistsAsync(excludeId, identityNumber))
        {
            throw new Exception("Identity number already exists.");
        }
    }

    private async Task CheckPassportNumberNotExistAsync(Guid? excludeId, string passportNumber)
    {
        if (await patientRepository.PassportNumberExistsAsync(excludeId, passportNumber))
        {
            throw new Exception("Passport number already exists.");
        }
    }
}