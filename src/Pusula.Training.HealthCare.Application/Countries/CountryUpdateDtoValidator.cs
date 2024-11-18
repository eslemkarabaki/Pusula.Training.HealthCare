using FluentValidation;

namespace Pusula.Training.HealthCare.Countries;

public class CountryUpdateDtoValidator : AbstractValidator<CountryUpdateDto>
{
    public CountryUpdateDtoValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(CountryConsts.NameMaxLength);
        RuleFor(e => e.Iso).NotEmpty().MaximumLength(CountryConsts.IsoMaxLength);
        RuleFor(e => e.PhoneCode).NotEmpty().MaximumLength(CountryConsts.PhoneCodeMaxLength);
    }
}