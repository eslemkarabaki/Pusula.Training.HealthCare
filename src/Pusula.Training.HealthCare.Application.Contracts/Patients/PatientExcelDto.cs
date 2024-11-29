using System;

namespace Pusula.Training.HealthCare.Patients;

public class PatientExcelDto
{
    public int No { get; set; }
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string IdentityNumber { get; set; } = null!;
    public string PassportNumber { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string MobilePhoneNumberCode { get; set; } = null!;
    public string MobilePhoneNumber { get; set; } = null!;
    public string? HomePhoneNumberCode { get; set; }
    public string? HomePhoneNumber { get; set; }
    public EnumGender Gender { get; set; }
    public EnumBloodType BloodType { get; set; }
    public EnumMaritalStatus MaritalStatus { get; set; }
    public string Race { get; set; } = null!;
    public string Type { get; set; } = null!;
}