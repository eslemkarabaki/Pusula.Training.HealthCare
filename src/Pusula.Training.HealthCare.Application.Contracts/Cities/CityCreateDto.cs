using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Cities;

public class CityCreateDto
{
    public string Name { get; set; } = null!;

    public Guid CountryId { get; set; }
}