using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Countries;

public class CountryUpdateDto
{
    public string Name { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;
}