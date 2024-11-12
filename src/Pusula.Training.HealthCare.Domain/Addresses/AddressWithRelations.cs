using System;
using Volo.Abp.Auditing;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressWithRelations : IHasCreationTime
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }

    public Guid CountryId { get; set; }
    public string Country { get; set; } = null!;

    public Guid CityId { get; set; }
    public string City { get; set; } = null!;

    public Guid DistrictId { get; set; }
    public string District { get; set; } = null!;

    public string AddressLine { get; set; } = null!;

    public virtual DateTime CreationTime { get; set; }
}