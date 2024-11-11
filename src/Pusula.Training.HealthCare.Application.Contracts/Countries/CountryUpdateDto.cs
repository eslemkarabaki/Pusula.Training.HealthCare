using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Countries;

public class CountryUpdateDto
{
    [Required]
    [StringLength(CountryConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(CountryConsts.AbbreviationMaxLength)]
    public string Abbreviation { get; set; } = null!;
}