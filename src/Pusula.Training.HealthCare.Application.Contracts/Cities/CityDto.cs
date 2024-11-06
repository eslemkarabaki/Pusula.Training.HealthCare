using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Cities;

public class CityDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid CountryId { get; set; }
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}