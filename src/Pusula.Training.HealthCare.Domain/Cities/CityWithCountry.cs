using System;
using Volo.Abp;
using Volo.Abp.Auditing;

namespace Pusula.Training.HealthCare.Cities;

public class CityWithCountry : IHasCreationTime
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CountryId { get; set; }
    public string Country { get; set; }
    public DateTime CreationTime { get; }
}