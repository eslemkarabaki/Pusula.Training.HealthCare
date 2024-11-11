using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid CityId { get; set; }
    public string City { get; set; }
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}