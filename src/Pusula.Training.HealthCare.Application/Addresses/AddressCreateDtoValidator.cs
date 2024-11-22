using FluentValidation;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressCreateDtoValidator : AbstractValidator<AddressCreateDto>
{
    public AddressCreateDtoValidator()
    {
        RuleFor(e => e.AddressTitle).NotEmpty().MaximumLength(AddressConsts.TitleMaxLength);
        RuleFor(e => e.AddressLine).NotEmpty().MaximumLength(AddressConsts.AddressMaxLength);
        RuleFor(e => e.DistrictId).NotEmpty();
    }
}