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
    AddressManager addressManager,
    PatientNoteManager patientNoteManager
) : DomainService
{
    public virtual async Task<Patient> CreateAsync(
        Guid countryId,
        Guid patientTypeId,
        Guid insuranceId,
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
        ICollection<Address> addresses,
        ICollection<PatientNote> notes
    )
    {
        var patient = new Patient(
            GuidGenerator.Create(),
            countryId,
            patientTypeId,
            insuranceId,
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
        await patientNoteManager.CreateNotesAsync(patient.Id, notes);
        return await patientRepository.InsertAsync(patient);
    }

    public virtual async Task<Patient> UpdateAsync(
        Guid id,
        Guid countryId,
        Guid patientTypeId,
        Guid insuranceId,
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
        ICollection<PatientNote> notes,
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
        patient.SetInsuranceId(insuranceId);

        await addressManager.SetAddressesAsync(patient.Id, addresses);
        await patientNoteManager.SetNotesAsync(patient.Id, notes);
        patient.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await patientRepository.UpdateAsync(patient);
    }
}