using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Patients;

public class PatientUpdateDto : IHasConcurrencyStamp
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string IdentityNumber { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string MobilePhoneNumber { get; set; } = null!;

    public string? HomePhoneNumber { get; set; }

    public EnumGender Gender { get; set; }

    public EnumBloodType BloodType { get; set; }

    public EnumMaritalStatus MaritalStatus { get; set; }

    public string Address { get; set; } = null!;

    public Guid DistrictId { get; set; }

    public Guid CountryId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}