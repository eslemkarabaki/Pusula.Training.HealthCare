using FluentValidation;

namespace Pusula.Training.HealthCare.Cities;

public class CityCreateDtoValidator : AbstractValidator<CityCreateDto>
{
    public CityCreateDtoValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(CityConsts.NameMaxLength);
        RuleFor(e => e.CountryId).NotEmpty();
    }
}