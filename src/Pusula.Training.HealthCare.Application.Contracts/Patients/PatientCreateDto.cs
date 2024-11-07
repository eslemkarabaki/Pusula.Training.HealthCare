using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Pusula.Training.HealthCare.Patients;

public class PatientCreateDto
{
    [Required]
    [StringLength(PatientConsts.FirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(PatientConsts.LastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [Required] public DateTime BirthDate { get; set; }

    [Required]
    [StringLength(PatientConsts.IdentityNumberMaxLength)]
    public string IdentityNumber { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(PatientConsts.EmailAddressMaxLength)]
    public string EmailAddress { get; set; } = null!;

    [Required]
    [Phone]
    [StringLength(PatientConsts.PhoneNumberMaxLength)]
    public string MobilePhoneNumber { get; set; } = null!;

    [Phone]
    [StringLength(PatientConsts.PhoneNumberMaxLength)]
    public string? HomePhoneNumber { get; set; }

    [Required] public EnumGender Gender { get; set; }

    [Required] public EnumBloodType BloodType { get; set; }

    [Required] public EnumMaritalStatus MaritalStatus { get; set; }

    public Guid CountryId { get; set; }
}