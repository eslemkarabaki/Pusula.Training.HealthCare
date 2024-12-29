using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Countries;

public class CountryCreateDto
{
    [Required]
    [StringLength(CountryConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(CountryConsts.IsoMaxLength)]
    public string Iso { get; set; } = null!;

    [Required]
    [StringLength(CountryConsts.PhoneCodeMaxLength)]
    public string PhoneCode { get; set; } = null!;
}