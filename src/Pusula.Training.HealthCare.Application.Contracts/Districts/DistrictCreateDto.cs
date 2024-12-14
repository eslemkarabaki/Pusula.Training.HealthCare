using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.DataAnnotations;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictCreateDto
{
    [Required]
    [StringLength(DistrictConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [NotEmptyGuid]
    public Guid CityId { get; set; }
}