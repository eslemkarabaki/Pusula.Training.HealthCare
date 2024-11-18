using System;
using System.Collections.Generic;
using Pusula.Training.HealthCare.Addresses;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Patients;

public class PatientView : IPatient
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? IdentityNumber { get; set; }
    public string? PassportNumber { get; set; }
    public string EmailAddress { get; set; }
    public string MobilePhoneNumberCode { get; set; }
    public string MobilePhoneNumber { get; set; }
    public string? HomePhoneNumberCode { get; set; }
    public string? HomePhoneNumber { get; set; }
    public EnumGender Gender { get; set; }
    public EnumBloodType BloodType { get; set; }
    public EnumMaritalStatus MaritalStatus { get; set; }
    public Guid CountryId { get; set; }
    public Guid PatientTypeId { get; set; }

    public string Country { get; set; }
    public string PatientType { get; set; }

    public IEnumerable<AddressView> Addresses { get; set; }
}