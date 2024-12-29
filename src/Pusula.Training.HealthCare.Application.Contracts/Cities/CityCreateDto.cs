using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;

namespace Pusula.Training.HealthCare.Cities;

public class CityCreateDto
{
    [Required]
    [StringLength(CityConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [NotEmptyGuid]
    public Guid CountryId { get; set; }
}