using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid PatientId { get; set; }

    public Guid CountryId { get; set; }
    public string Country { get; set; } = null!;

    public Guid CityId { get; set; }
    public string City { get; set; } = null!;

    public Guid DistrictId { get; set; }
    public string District { get; set; } = null!;

    public string AddressLine { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}