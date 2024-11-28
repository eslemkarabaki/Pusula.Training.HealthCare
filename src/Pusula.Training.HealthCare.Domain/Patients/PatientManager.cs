using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.PatientNotes;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Patients;

public class PatientManager(
    IPatientRepository patientRepository,
    IPatientNoteRepository patientNoteRepository,
    AddressManager addressManager
) : DomainService
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
        IEnumerable<Address> addresses
    )
    {
        await CheckIdentityAndPassportNumberAsync(identityNumber, passportNumber);

        var patient = new Patient(
            GuidGenerator.Create(),
            countryId,
            patientTypeId,
            firstName,
            lastName,
            birthDate,
            identityNumber,
            passportNumber,
            emailAddress,
            mobilePhoneNumberCode,
            mobilePhoneNumber,
            homePhoneNumberCode,
            homePhoneNumber,
            gender,
            bloodType,
            maritalStatus
        );
        await addressManager.CreateAddressesAsync(patient.Id, addresses);
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
        patient.SetName(firstName, lastName);
        patient.SetBirthDate(birthDate);
        patient.SetIdentityNumber(identityNumber);
        patient.SetPassportNumber(passportNumber);
        patient.SetEmailAddress(emailAddress);
        patient.SetMobilePhoneNumber(mobilePhoneNumber);
        patient.SetMobilePhoneNumberCode(mobilePhoneNumberCode);
        patient.SetHomePhoneNumber(homePhoneNumber);
        patient.SetHomePhoneNumberCode(homePhoneNumberCode);
        patient.SetGender(gender);
        patient.SetBloodType(bloodType);
        patient.SetMaritalStatus(maritalStatus);
        patient.SetCountryId(countryId);
        patient.SetPatientTypeId(patientTypeId);

        await addressManager.SetAddressesAsync(patient.Id, addresses);
        patient.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await patientRepository.UpdateAsync(patient);
    }

    private async Task CheckIdentityAndPassportNumberAsync(
        string? identityNumber,
        string? passportNumber,
        Guid? excludeId = null
    )
    {
        if (identityNumber.IsNullOrWhiteSpace() && passportNumber.IsNullOrWhiteSpace())
        {
            throw new Exception("Identity number or passport number is required."); //todo: custom exception
        }

        if (!identityNumber.IsNullOrWhiteSpace())
        {
            await CheckIdentityNumberNotExistAsync(excludeId, identityNumber);
        } else
        {
            await CheckPassportNumberNotExistAsync(excludeId, passportNumber!);
        }
    }

    private async Task CheckIdentityNumberNotExistAsync(Guid? excludeId, string identityNumber)
    {
        if (await patientRepository.IdentityNumberExistsAsync(excludeId, identityNumber))
        {
            throw new Exception("Identity number already exists."); //todo: custom exception
        }
    }

    private async Task CheckPassportNumberNotExistAsync(Guid? excludeId, string passportNumber)
    {
        if (await patientRepository.PassportNumberExistsAsync(excludeId, passportNumber))
        {
            throw new Exception("Passport number already exists."); //todo: custom exception
        }
    }
}