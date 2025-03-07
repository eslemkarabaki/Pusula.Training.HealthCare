using Volo.Abp.Application.Dtos;
using System;
using System.Text;
using System.Web;

namespace Pusula.Training.HealthCare.Patients;

public class GetPatientsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }
    public int? No { get; set; }
    public string? FullName { get; set; }
    public DateTime? BirthDateMin { get; set; }
    public DateTime? BirthDateMax { get; set; }
    public string? IdentityNumber { get; set; }
    public string? PassportNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? MobilePhoneNumber { get; set; }
    public string? HomePhoneNumber { get; set; }
    public EnumGender Gender { get; set; } = EnumGender.None;
    public EnumBloodType BloodType { get; set; } = EnumBloodType.None;
    public EnumMaritalStatus MaritalStatus { get; set; } = EnumMaritalStatus.None;

    public Guid? CountryId { get; set; }
    public Guid? PatientTypeId { get; set; }
    public Guid? InsuranceId { get; set; }

    public string ToQueryParameterString(string? culture = null)
    {
        var parameters = new StringBuilder();
        if (!culture.IsNullOrWhiteSpace())
        {
            parameters.Append($"&culture={culture}");
        }

        parameters.Append($"&FilterText={HttpUtility.UrlEncode(FilterText)}");
        parameters.Append($"&No={No}");
        parameters.Append($"&FullName={HttpUtility.UrlEncode(FullName)}");
        parameters.Append($"&BirthDateMin={BirthDateMin?.ToString("O")}");
        parameters.Append($"&BirthDateMax={BirthDateMax?.ToString("O")}");
        parameters.Append($"&IdentityNumber={HttpUtility.UrlEncode(IdentityNumber)}");
        parameters.Append($"&IdentityNumber={HttpUtility.UrlEncode(PassportNumber)}");
        parameters.Append($"&EmailAddress={HttpUtility.UrlEncode(EmailAddress)}");
        parameters.Append($"&MobilePhoneNumber={HttpUtility.UrlEncode(MobilePhoneNumber)}");
        parameters.Append($"&HomePhoneNumber={HttpUtility.UrlEncode(HomePhoneNumber)}");
        parameters.Append($"&Gender={Gender}");
        parameters.Append($"&BloodType={BloodType}");
        parameters.Append($"&MaritalStatus={MaritalStatus}");
        parameters.Append($"&CountryId={CountryId}");
        parameters.Append($"&PatientTypeId={PatientTypeId}");
        parameters.Append($"&InsuranceId={InsuranceId}");
        return parameters.ToString();
    }

    public bool AreAllPropertiesEmpty() =>
        !No.HasValue &&
        string.IsNullOrEmpty(FilterText) &&
        string.IsNullOrEmpty(FullName) &&
        !BirthDateMin.HasValue &&
        !BirthDateMax.HasValue &&
        string.IsNullOrEmpty(IdentityNumber) &&
        string.IsNullOrEmpty(PassportNumber) &&
        string.IsNullOrEmpty(MobilePhoneNumber) &&
        string.IsNullOrEmpty(HomePhoneNumber) &&
        string.IsNullOrEmpty(EmailAddress) &&
        Gender == EnumGender.None &&
        BloodType == EnumBloodType.None &&
        MaritalStatus == EnumMaritalStatus.None;
}