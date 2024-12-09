using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.DataAnnotations;
using Pusula.Training.HealthCare.PatientNotes;

namespace Pusula.Training.HealthCare.Patients;

public class PatientCreateDto
{
    [Required]
    [StringLength(PatientConsts.FirstNameMaxLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(PatientConsts.LastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [Required]
    [DateRange("1900-01-01", "now")]
    public DateTime BirthDate { get; set; } = DateTime.Today;

    [RequiredIf(nameof(PassportNumber), null)]
    [StringLength(PatientConsts.IdentityNumberMaxLength)]
    public string? IdentityNumber { get; set; }

    [RequiredIf(nameof(IdentityNumber), null)]
    [StringLength(PatientConsts.PassportNumberMaxLength)]
    public string? PassportNumber { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(PatientConsts.EmailAddressMaxLength)]
    public string EmailAddress { get; set; } = null!;

    [Required]
    [StringLength(CountryConsts.PhoneCodeMaxLength)]
    public string MobilePhoneNumberCode { get; set; } = null!;

    [Required]
    [StringLength(PatientConsts.PhoneNumberMaxLength)]
    public string MobilePhoneNumber { get; set; } = null!;

    [StringLength(CountryConsts.PhoneCodeMaxLength)]
    public string? HomePhoneNumberCode { get; set; }

    [StringLength(CountryConsts.PhoneCodeMaxLength)]
    public string? HomePhoneNumber { get; set; }

    [Required]
    [DeniedValues(EnumGender.None)]
    public EnumGender Gender { get; set; }

    [Required]
    [DeniedValues(EnumBloodType.None)]
    public EnumBloodType BloodType { get; set; }

    [Required]
    [DeniedValues(EnumMaritalStatus.None)]
    public EnumMaritalStatus MaritalStatus { get; set; }

    [Required] [NotEmptyGuid] public Guid CountryId { get; set; }

    [Required] [NotEmptyGuid] public Guid PatientTypeId { get; set; }

    public ICollection<AddressCreateDto> Addresses { get; set; } = [];
    public ICollection<PatientNoteCreateDto> Notes { get; set; } = [];
}