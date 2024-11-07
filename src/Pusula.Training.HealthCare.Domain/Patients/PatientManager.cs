using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Patients;

public class PatientManager(IPatientRepository patientRepository, AddressManager addressManager) : DomainService
{
    public virtual async Task<Patient> CreateAsync(
        Guid countryId,
        string firstName,
        string lastName,
        DateTime birthDate,
        string identityNumber,
        string emailAddress,
        string mobilePhoneNumber,
        EnumGender gender,
        EnumBloodType bloodType,
        EnumMaritalStatus maritalStatus,
        Guid districtId,
        string address,
        string? homePhoneNumber = null)
    {
        Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));
        Check.NotNullOrWhiteSpace(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength);
        Check.NotNullOrWhiteSpace(lastName, nameof(lastName), PatientConsts.LastNameMaxLength);
        Check.NotNullOrWhiteSpace(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength);
        Check.NotNullOrWhiteSpace(emailAddress, nameof(emailAddress), PatientConsts.EmailAddressMaxLength);
        Check.NotNullOrWhiteSpace(mobilePhoneNumber, nameof(mobilePhoneNumber), PatientConsts.PhoneNumberMaxLength);

        if (!string.IsNullOrWhiteSpace(homePhoneNumber))
        {
            Check.Length(homePhoneNumber, nameof(homePhoneNumber), PatientConsts.PhoneNumberMaxLength);
        }

        var patient = new Patient(
            GuidGenerator.Create(), countryId, firstName, lastName, birthDate, identityNumber, emailAddress,
            mobilePhoneNumber, gender, bloodType, maritalStatus, homePhoneNumber
        );
        await addressManager.CreateAsync(patient.Id, districtId, address);
        return await patientRepository.InsertAsync(patient);
    }

    public virtual async Task<Patient> UpdateAsync(
        Guid id,
        Guid countryId,
        string firstName,
        string lastName,
        DateTime birthDate,
        string identityNumber,
        string emailAddress,
        string mobilePhoneNumber,
        EnumGender gender,
        EnumBloodType bloodType,
        EnumMaritalStatus maritalStatus,
        Guid districtId,
        string address,
        string? homePhoneNumber = null,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));
        Check.NotNullOrWhiteSpace(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength);
        Check.NotNullOrWhiteSpace(lastName, nameof(lastName), PatientConsts.LastNameMaxLength);
        Check.NotNullOrWhiteSpace(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength);
        Check.NotNullOrWhiteSpace(emailAddress, nameof(emailAddress), PatientConsts.EmailAddressMaxLength);
        Check.NotNullOrWhiteSpace(mobilePhoneNumber, nameof(mobilePhoneNumber), PatientConsts.PhoneNumberMaxLength);

        if (!string.IsNullOrWhiteSpace(homePhoneNumber))
        {
            Check.Length(homePhoneNumber, nameof(homePhoneNumber), PatientConsts.PhoneNumberMaxLength);
        }

        var patient = await patientRepository.GetAsync(id);

        patient.CountryId = countryId;
        patient.FirstName = firstName;
        patient.LastName = lastName;
        patient.BirthDate = birthDate;
        patient.IdentityNumber = identityNumber;
        patient.EmailAddress = emailAddress;
        patient.MobilePhoneNumber = mobilePhoneNumber;
        patient.Gender = gender;
        patient.BloodType = bloodType;
        patient.MaritalStatus = maritalStatus;
        patient.HomePhoneNumber = homePhoneNumber;

        patient.SetConcurrencyStampIfNotNull(concurrencyStamp);

        await addressManager.UpdateAsync(patient.Id, districtId, address);
        return await patientRepository.UpdateAsync(patient);
    }
}