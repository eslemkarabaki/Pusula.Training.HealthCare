using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Countries;

public class CountryDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; } = null!;
    public string Iso { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
    public bool IsCurrent { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}