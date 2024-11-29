using System;

namespace Pusula.Training.HealthCare.Addresses;

public class AddressUpdateDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid DistrictId { get; set; }
    public string AddressTitle { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
}