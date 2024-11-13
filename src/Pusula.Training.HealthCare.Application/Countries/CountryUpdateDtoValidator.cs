using FluentValidation;

namespace Pusula.Training.HealthCare.Countries;

public class CountryUpdateDtoValidator : AbstractValidator<CountryUpdateDto>
{
    public CountryUpdateDtoValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(CountryConsts.NameMaxLength);
        RuleFor(e => e.Abbreviation).NotEmpty().MaximumLength(CountryConsts.AbbreviationMaxLength);
    }
}