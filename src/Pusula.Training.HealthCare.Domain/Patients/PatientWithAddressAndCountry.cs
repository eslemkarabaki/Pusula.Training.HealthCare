using System;
using Pusula.Training.HealthCare.Addresses;
using Volo.Abp.Auditing;

namespace Pusula.Training.HealthCare.Patients;

public class PatientWithAddressAndCountry : IHasCreationTime
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string IdentityNumber { get; set; }
    public string EmailAddress { get; set; }
    public string MobilePhoneNumber { get; set; }
    public string? HomePhoneNumber { get; set; }
    public EnumGender Gender { get; set; }
    public EnumBloodType BloodType { get; set; }
    public EnumMaritalStatus MaritalStatus { get; set; }

    public Guid CountryId { get; set; }
    public string Country { get; set; }

    public AddressWithRelations Address { get; set; }

    public DateTime CreationTime { get; }
}