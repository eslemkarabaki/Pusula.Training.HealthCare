using System;
using Volo.Abp.Auditing;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressWithRelations : IHasCreationTime
{
    public virtual Guid Id { get; set; }
    public virtual Guid PatientId { get; set; }
    public virtual string Country { get; set; } = null!;
    public virtual string City { get; set; } = null!;
    public virtual string District { get; set; } = null!;
    public virtual string AddressLine { get; set; } = null!;
    public virtual DateTime CreationTime { get; }
}