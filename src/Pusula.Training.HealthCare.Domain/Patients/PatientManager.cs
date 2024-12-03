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
        ICollection<Address> addresses
    )
    {
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
        string emailAddress,
        string mobilePhoneNumberCode,
        string mobilePhoneNumber,
        string? homePhoneNumberCode,
        string? homePhoneNumber,
        EnumGender gender,
        EnumBloodType bloodType,
        EnumMaritalStatus maritalStatus,
        ICollection<Address> addresses,
        string? concurrencyStamp = null
    )
    {
        var patient = await patientRepository.GetAsync(id);
        patient.SetName(firstName, lastName);
        patient.SetBirthDate(birthDate);
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
}