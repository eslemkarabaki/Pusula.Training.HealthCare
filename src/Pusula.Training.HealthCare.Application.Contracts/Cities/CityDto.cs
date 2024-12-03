using System;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Cities;

public class CityDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid CountryId { get; set; }
    public CountryDto Country { get; set; } = null!;
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}