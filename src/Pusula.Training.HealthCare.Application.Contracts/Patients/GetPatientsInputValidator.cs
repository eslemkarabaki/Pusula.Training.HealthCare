using System;
using FluentValidation;

namespace Pusula.Training.HealthCare.Patients;

public class GetPatientsInputValidator : AbstractValidator<GetPatientsInput>
{
    public GetPatientsInputValidator()
    {
        RuleFor(e => e.FilterText)
            .Length(3,128)
            .When(e => !e.FilterText.IsNullOrWhiteSpace())
            .WithMessage("The filter text must be between {minLength} and {maxLength} characters.");
        
        RuleFor(e => e.No)
            .GreaterThan(0)
            .LessThan(int.MaxValue);

        RuleFor(e => e.FullName)
            .Length(3,PatientConsts.FirstNameMaxLength + PatientConsts.LastNameMaxLength)
            .When(e => !e.FullName.IsNullOrWhiteSpace())
            .WithMessage("The name must be between {minLength} and {maxLength} characters.");

        RuleFor(e => e.PassportNumber)
            .Length(3,PatientConsts.PassportNumberMaxLength)
            .When(e => !e.PassportNumber.IsNullOrWhiteSpace())
            .WithMessage("The passport number must be between {minLength} and {maxLength} characters.");

        RuleFor(e => e.IdentityNumber)
            .Length(3, PatientConsts.IdentityNumberMaxLength)
            .When(e => !e.IdentityNumber.IsNullOrWhiteSpace())
            .WithMessage("The identity number be between {minLength} and {maxLength} characters.");

        RuleFor(e => e.MobilePhoneNumber)
            .Length(3, PatientConsts.PhoneNumberMaxLength)
            .When(e => !e.MobilePhoneNumber.IsNullOrWhiteSpace())
            .WithMessage("The mobile phone number must be between {minLength} and {maxLength} characters.");
    }
}