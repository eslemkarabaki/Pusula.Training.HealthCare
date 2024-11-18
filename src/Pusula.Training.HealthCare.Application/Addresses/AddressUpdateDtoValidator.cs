using FluentValidation;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressUpdateDtoValidator : AbstractValidator<AddressUpdateDto>
{
    public AddressUpdateDtoValidator()
    {
        RuleFor(e => e.AddressTitle).NotEmpty().MaximumLength(AddressConsts.TitleMaxLength);
        RuleFor(e => e.AddressLine).NotEmpty().MaximumLength(AddressConsts.AddressMaxLength);
        RuleFor(e => e.DistrictId).NotEmpty();
    }
}