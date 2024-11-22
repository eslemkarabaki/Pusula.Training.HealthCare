using FluentValidation;

namespace Pusula.Training.HealthCare.Countries;

public class CountryCreateDtoValidator : AbstractValidator<CountryCreateDto>
{
    public CountryCreateDtoValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(CountryConsts.NameMaxLength);
        RuleFor(e => e.Iso).NotEmpty().MaximumLength(CountryConsts.IsoMaxLength);
        RuleFor(e => e.PhoneCode).NotEmpty().MaximumLength(CountryConsts.PhoneCodeMaxLength);
    }
}