using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid PatientId { get; set; }
    public Guid DistrictId { get; set; }
    public string AddressLine { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}