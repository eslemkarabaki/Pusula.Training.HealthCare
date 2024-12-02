using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.Countries;

namespace Pusula.Training.HealthCare.Cities;

public class CityUpdateDto
{
    public string Name { get; set; } = null!;

    public Guid CountryId { get; set; }
    public CountryUpdateDto Country { get; set; } = new();
}