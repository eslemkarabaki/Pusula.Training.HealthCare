using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Patients;

public class Patient : FullAuditedAggregateRoot<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string IdentityNumber { get; set; }
    public string EmailAddress { get; set; }
    public string MobilePhoneNumber { get; set; }
    public string? HomePhoneNumber { get; set; }
    public EnumGender Gender { get; set; }
    public EnumBloodType BloodType { get; set; }
    public EnumMaritalStatus MaritalStatus { get; set; }
    public DateTime RegisterDate { get; set; }

    public Guid CountryId { get; set; }

    protected Patient()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        IdentityNumber = string.Empty;
        EmailAddress = string.Empty;
        MobilePhoneNumber = string.Empty;
        Gender = EnumGender.None;
        BloodType = EnumBloodType.None;
        MaritalStatus = EnumMaritalStatus.None;
    }

    public Patient(Guid id, Guid countryId, string firstName, string lastName, DateTime birthDate,
        string identityNumber,
        string emailAddress, string mobilePhoneNumber, EnumGender gender, EnumBloodType bloodType,
        EnumMaritalStatus maritalStatus, string? homePhoneNumber = null)
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

        Id = id;
        CountryId = countryId;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        IdentityNumber = identityNumber;
        EmailAddress = emailAddress;
        MobilePhoneNumber = mobilePhoneNumber;
        Gender = gender;
        BloodType = bloodType;
        MaritalStatus = maritalStatus;
        HomePhoneNumber = homePhoneNumber;
        RegisterDate = DateTime.Now;
    }
}