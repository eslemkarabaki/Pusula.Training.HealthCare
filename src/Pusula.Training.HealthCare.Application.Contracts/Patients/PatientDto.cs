using System;
using Pusula.Training.HealthCare.Addresses;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Patients;

public class PatientDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp, IPatient
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullName => $"{FirstName} {LastName}";
    public DateTime BirthDate { get; set; }

    public Tuple<int, string> Age => CalculateAge();

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

    public string ConcurrencyStamp { get; set; } = null!;

    private Tuple<int, string> CalculateAge()
    {
        var totalDays = (DateTime.Today - BirthDate).TotalDays;
        var year = totalDays / 365.2425;
        if (year < 1)
        {
            if (totalDays < 30)
            {
                return new Tuple<int, string>((int)totalDays, "gün");
            }
            else
            {
                return new Tuple<int, string>((int)totalDays / 30, "ay");
            }
        }
        else
        {
            return new Tuple<int, string>((int)year, "yıl");
        }
    }
}