using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.Cities;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictUpdateDto
{
    public string Name { get; set; } = null!;

    public Guid CityId { get; set; }
    public CityUpdateDto City { get; set; } = new();
}