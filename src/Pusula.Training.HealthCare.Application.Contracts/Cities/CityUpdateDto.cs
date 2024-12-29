using System;
using System.ComponentModel.DataAnnotations;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Cities;

public class CityUpdateDto : IHasConcurrencyStamp
{
    [Required]
    [StringLength(CityConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [NotEmptyGuid]
    public Guid CountryId { get; set; }

    public CountryUpdateDto Country { get; set; } = new();

    public string ConcurrencyStamp { get; set; } = null!;
}