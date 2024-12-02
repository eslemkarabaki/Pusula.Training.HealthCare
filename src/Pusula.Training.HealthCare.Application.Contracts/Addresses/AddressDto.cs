using System;
using Pusula.Training.HealthCare.Districts;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp, IAddress
{
    public Guid PatientId { get; set; }
    public Guid DistrictId { get; set; }
    public DistrictDto District { get; set; } = null!;
    public string AddressTitle { get; set; } = null!;
    public string AddressLine { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}