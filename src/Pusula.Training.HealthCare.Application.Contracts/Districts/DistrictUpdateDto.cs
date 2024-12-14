using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(DistrictConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [NotEmptyGuid]
    public Guid CityId { get; set; }

    public CityUpdateDto City { get; set; } = new();

    public string ConcurrencyStamp { get; set; } = null!;
}