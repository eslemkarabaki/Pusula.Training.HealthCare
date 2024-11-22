using FluentValidation;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictUpdateDtoValidator : AbstractValidator<DistrictUpdateDto>
{
    public DistrictUpdateDtoValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(DistrictConsts.NameMaxLength);
        RuleFor(e => e.CityId).NotEmpty();
    }
}