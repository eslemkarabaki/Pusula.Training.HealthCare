using System;
using FluentValidation;
using Pusula.Training.HealthCare.Addresses;

namespace Pusula.Training.HealthCare.Patients;

public class PatientUpdateDtoValidator : AbstractValidator<PatientUpdateDto>
{
    public PatientUpdateDtoValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(PatientConsts.FirstNameMaxLength);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(PatientConsts.LastNameMaxLength);
        RuleFor(e => e.Address).NotEmpty().MaximumLength(AddressConsts.AddressMaxLength);
        RuleFor(e => e.BirthDate).NotEmpty().ExclusiveBetween(new DateTime(1900, 1, 1), DateTime.Now);
        RuleFor(e => e.EmailAddress).NotEmpty().EmailAddress().MaximumLength(PatientConsts.EmailAddressMaxLength);
        RuleFor(e => e.IdentityNumber).NotEmpty().MaximumLength(PatientConsts.IdentityNumberMaxLength);
        RuleFor(e => e.HomePhoneNumber).NotEmpty().MaximumLength(PatientConsts.PhoneNumberMaxLength);   // regex
        RuleFor(e => e.MobilePhoneNumber).NotEmpty().MaximumLength(PatientConsts.PhoneNumberMaxLength); // regex
        RuleFor(e => e.CountryId).NotEmpty();
        RuleFor(e => e.DistrictId).NotEmpty();
        RuleFor(e => e.Gender).NotEmpty().NotEqual(EnumGender.None);
        RuleFor(e => e.BloodType).NotEmpty().NotEqual(EnumBloodType.None);
        RuleFor(e => e.MaritalStatus).NotEmpty().NotEqual(EnumMaritalStatus.None);
    }
}