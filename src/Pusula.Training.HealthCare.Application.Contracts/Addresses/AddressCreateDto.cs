using System;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressCreateDto
{
    public Guid DistrictId { get; set; }
    public string AddressTitle { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
}