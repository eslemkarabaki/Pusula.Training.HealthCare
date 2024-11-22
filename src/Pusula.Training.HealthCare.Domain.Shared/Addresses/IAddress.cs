using System;

namespace Pusula.Training.HealthCare.Addresses;

public interface IAddress
{
    Guid PatientId { get; }
    Guid DistrictId { get; }
    string AddressTitle { get; }
    string AddressLine { get; }
}