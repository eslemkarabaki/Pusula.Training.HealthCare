using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Addresses;

namespace Pusula.Training.HealthCare.Patients;

public class PatientCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string? IdentityNumber { get; set; }
    public string? PassportNumber { get; set; }
    public string EmailAddress { get; set; } = null!;
    public string MobilePhoneNumberCode { get; set; } = null!;
    public string MobilePhoneNumber { get; set; } = null!;
    public string? HomePhoneNumberCode { get; set; }
    public string? HomePhoneNumber { get; set; }
    public EnumGender Gender { get; set; }
    public EnumBloodType BloodType { get; set; }
    public EnumMaritalStatus MaritalStatus { get; set; }
    public Guid CountryId { get; set; }
    public Guid PatientTypeId { get; set; }

    public ICollection<AddressCreateDto> Addresses { get; set; } = [];
}