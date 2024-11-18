using System;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Patients;

public sealed class Patient : FullAuditedAggregateRoot<Guid>, IPatient
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? IdentityNumber { get; private set; }
    public string? PassportNumber { get; private set; }
    public string EmailAddress { get; private set; }
    public string MobilePhoneNumberCode { get; private set; }
    public string MobilePhoneNumber { get; private set; }
    public string? HomePhoneNumberCode { get; private set; }
    public string? HomePhoneNumber { get; private set; }
    public EnumGender Gender { get; private set; }
    public EnumBloodType BloodType { get; private set; }
    public EnumMaritalStatus MaritalStatus { get; private set; }
    public Guid CountryId { get; private set; }
    public Guid PatientTypeId { get; private set; }

    private Patient()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        EmailAddress = string.Empty;
        MobilePhoneNumber = string.Empty;
        MobilePhoneNumberCode = string.Empty;
    }


    internal Patient(Guid id, Guid countryId, Guid patientTypeId, string firstName, string lastName, DateTime birthDate,
                     string? identityNumber, string? passportNumber,
                     string emailAddress, string mobilePhoneNumberCode, string mobilePhoneNumber,
                     string? homePhoneNumberCode, string? homePhoneNumber, EnumGender gender,
                     EnumBloodType bloodType,
                     EnumMaritalStatus maritalStatus) : base(id)
    {
        Set(countryId, patientTypeId, firstName, lastName, birthDate, identityNumber, passportNumber, emailAddress,
            mobilePhoneNumberCode, mobilePhoneNumber, homePhoneNumberCode, homePhoneNumber, gender, bloodType,
            maritalStatus);
    }

    internal void Set(Guid countryId, Guid patientTypeId, string firstName, string lastName, DateTime birthDate,
                      string? identityNumber, string? passportNumber,
                      string emailAddress, string mobilePhoneNumberCode, string mobilePhoneNumber,
                      string? homePhoneNumberCode, string? homePhoneNumber, EnumGender gender,
                      EnumBloodType bloodType,
                      EnumMaritalStatus maritalStatus)
    {
        CountryId = Check.NotDefaultOrNull<Guid>(countryId, nameof(countryId));
        PatientTypeId = Check.NotDefaultOrNull<Guid>(patientTypeId, nameof(patientTypeId));
        FirstName = Check.NotNullOrWhiteSpace(firstName, nameof(firstName), PatientConsts.FirstNameMaxLength);
        LastName = Check.NotNullOrWhiteSpace(lastName, nameof(lastName), PatientConsts.LastNameMaxLength);
        BirthDate = birthDate;

        IdentityNumber =
            Check.Length(identityNumber, nameof(identityNumber), PatientConsts.IdentityNumberMaxLength);

        PassportNumber =
            Check.Length(passportNumber, nameof(passportNumber), PatientConsts.PassportNumberMaxLength);

        EmailAddress =
            Check.NotNullOrWhiteSpace(emailAddress, nameof(emailAddress), PatientConsts.EmailAddressMaxLength);

        MobilePhoneNumberCode = Check.NotNullOrWhiteSpace(mobilePhoneNumberCode, nameof(mobilePhoneNumberCode),
            CountryConsts.PhoneCodeMaxLength);

        MobilePhoneNumber = Check.NotNullOrWhiteSpace(mobilePhoneNumber, nameof(mobilePhoneNumber),
            PatientConsts.PhoneNumberMaxLength);

        HomePhoneNumberCode =
            Check.Length(homePhoneNumberCode, nameof(homePhoneNumberCode), CountryConsts.PhoneCodeMaxLength);

        HomePhoneNumber =
            Check.Length(homePhoneNumber, nameof(homePhoneNumber), PatientConsts.PhoneNumberMaxLength);

        Gender = gender;
        BloodType = bloodType;
        MaritalStatus = maritalStatus;
    }
}