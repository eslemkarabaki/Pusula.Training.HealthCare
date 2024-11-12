using System;
using Volo.Abp;
using Volo.Abp.Auditing;

namespace Pusula.Training.HealthCare.Districts;

public class DistrictWithCity : IHasCreationTime
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CityId { get; set; }
    public string City { get; set; }
    public DateTime CreationTime { get; }
}