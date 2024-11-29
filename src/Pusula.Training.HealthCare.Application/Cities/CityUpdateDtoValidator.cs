using FluentValidation;

namespace Pusula.Training.HealthCare.Cities;

public class CityUpdateDtoValidator : AbstractValidator<CityUpdateDto>
{
    public CityUpdateDtoValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(CityConsts.NameMaxLength);
        RuleFor(e => e.CountryId).NotEmpty();
    }
}