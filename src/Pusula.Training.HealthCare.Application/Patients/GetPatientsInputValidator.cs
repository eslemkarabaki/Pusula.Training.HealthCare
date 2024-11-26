using System;
using FluentValidation;

namespace Pusula.Training.HealthCare.Patients;

public class GetPatientsInputValidator : AbstractValidator<GetPatientsInput>
{
    public GetPatientsInputValidator()
    {
        RuleFor(e => e.FilterText)
            .MinimumLength(3)
            .When(e => !e.FilterText.IsNullOrWhiteSpace())
            .WithMessage("The filter text must be at least {minimumLength} characters long.");

        RuleFor(e => e.PassportNumber)
            .MinimumLength(3)
            .When(e => !e.PassportNumber.IsNullOrWhiteSpace())
            .WithMessage("The passport number must be at least {minimumLength} characters long.");

        RuleFor(e => e.IdentityNumber)
            .MinimumLength(3)
            .When(e => !e.IdentityNumber.IsNullOrWhiteSpace())
            .WithMessage("The passport number must be at least {minimumLength} characters long.");

        RuleFor(e => e.MobilePhoneNumber)
            .MinimumLength(3)
            .When(e => !e.MobilePhoneNumber.IsNullOrWhiteSpace())
            .WithMessage("The passport number must be at least {minimumLength} characters long.");
    }
}