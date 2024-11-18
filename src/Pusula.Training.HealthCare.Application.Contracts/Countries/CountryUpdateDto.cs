using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Countries;

public class CountryUpdateDto
{
    public string Name { get; set; } = null!;
    public string Iso { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
    public bool IsCurrent { get; set; }
}