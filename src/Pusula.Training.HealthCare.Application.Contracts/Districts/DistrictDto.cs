using System;
using Pusula.Training.HealthCare.Cities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictDto : EntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid CityId { get; set; }
    public CityDto City { get; set; } = null!;
    public string Name { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}