using System;

namespace Pusula.Training.HealthCare.Patients;

public interface IPatient
{
    Guid Id { get; }
    string FirstName { get; }
    string LastName { get; }
    DateTime BirthDate { get; }
    string? IdentityNumber { get; }
    string? PassportNumber { get; }
    string EmailAddress { get; }
    public string MobilePhoneNumberCode { get; }
    string MobilePhoneNumber { get; }
    public string? HomePhoneNumberCode { get; }
    string? HomePhoneNumber { get; }
    EnumGender Gender { get; }
    EnumBloodType BloodType { get; }
    EnumMaritalStatus MaritalStatus { get; }
    Guid CountryId { get; }
    Guid PatientTypeId { get; }
}