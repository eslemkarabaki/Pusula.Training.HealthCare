using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.Cities;

public class CityCreateDto
{
    [Required]
    [StringLength(CityConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required] public Guid CountryId { get; set; }
}