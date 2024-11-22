using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictUpdateDto
{
    public string Name { get; set; } = null!;

    public Guid CityId { get; set; }
}