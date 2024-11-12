using System;

namespace Pusula.Training.HealthCare.Patients;

public class PatientExcelDownloadDto
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDateMin { get; set; }
    public DateTime? BirthDateMax { get; set; }
    public string? IdentityNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? MobilePhoneNumber { get; set; }
    public string? HomePhoneNumber { get; set; }
    public EnumGender Gender { get; set; } = EnumGender.None;
    public EnumBloodType BloodType { get; set; } = EnumBloodType.None;
    public EnumMaritalStatus MaritalStatus { get; set; } = EnumMaritalStatus.None;

    public Guid? CountryId { get; set; }
}