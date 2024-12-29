using System;
using FluentValidation;
using Pusula.Training.HealthCare.Countries;

namespace Pusula.Training.HealthCare.Patients;

public class PatientCreateDtoValidator : AbstractValidator<PatientCreateDto>
{
    public PatientCreateDtoValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(PatientConsts.FirstNameMaxLength);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(PatientConsts.LastNameMaxLength);
        RuleFor(e => e.BirthDate).NotEmpty().ExclusiveBetween(new DateTime(1900, 1, 1), DateTime.Today.AddDays(1));

        RuleFor(e => e.IdentityNumber)
            .NotEmpty()
            .When(e => string.IsNullOrEmpty(e.PassportNumber))
            .MaximumLength(PatientConsts.IdentityNumberMaxLength);

        RuleFor(e => e.PassportNumber)
            .NotEmpty()
            .When(e => string.IsNullOrEmpty(e.IdentityNumber))
            .MaximumLength(PatientConsts.PassportNumberMaxLength);

        RuleFor(e => e.EmailAddress).NotEmpty().EmailAddress().MaximumLength(PatientConsts.EmailAddressMaxLength);
        RuleFor(e => e.MobilePhoneNumberCode).NotEmpty().MaximumLength(CountryConsts.PhoneCodeMaxLength);
        RuleFor(e => e.MobilePhoneNumber).NotEmpty().MaximumLength(PatientConsts.PhoneNumberMaxLength);
        RuleFor(e => e.HomePhoneNumberCode).MaximumLength(CountryConsts.PhoneCodeMaxLength);
        RuleFor(e => e.HomePhoneNumber).MaximumLength(PatientConsts.PhoneNumberMaxLength);

        RuleFor(e => e.Gender).NotEmpty().NotEqual(EnumGender.None);
        RuleFor(e => e.BloodType).NotEmpty().NotEqual(EnumBloodType.None);
        RuleFor(e => e.MaritalStatus).NotEmpty().NotEqual(EnumMaritalStatus.None);

        RuleFor(e => e.CountryId).NotEmpty();
        RuleFor(e => e.PatientTypeId).NotEmpty();
        RuleFor(e => e.InsuranceId).NotEmpty();
    }
}