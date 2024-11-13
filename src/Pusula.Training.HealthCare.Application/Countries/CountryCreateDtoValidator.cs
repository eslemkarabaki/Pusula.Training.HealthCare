using FluentValidation;

namespace Pusula.Training.HealthCare.Countries;

public class CountryCreateDtoValidator : AbstractValidator<CountryCreateDto>
{
    public CountryCreateDtoValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(CountryConsts.NameMaxLength);
        RuleFor(e => e.Abbreviation).NotEmpty().MaximumLength(CountryConsts.AbbreviationMaxLength);
    }
}