using FluentValidation;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictCreateDtoValidator : AbstractValidator<DistrictCreateDto>
{
    public DistrictCreateDtoValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(DistrictConsts.NameMaxLength);
        RuleFor(e => e.CityId).NotEmpty();
    }
}