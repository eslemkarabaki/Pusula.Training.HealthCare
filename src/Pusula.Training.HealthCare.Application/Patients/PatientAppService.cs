using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.GlobalExceptions;
using Pusula.Training.HealthCare.PatientNotes;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.EventBus.Distributed;

namespace Pusula.Training.HealthCare.Patients;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Patients.Default)]
public class PatientAppService(
    IPatientRepository patientRepository,
    IAddressRepository addressRepository,
    PatientManager patientManager,
    IPatientRules patientRules,
    IDistributedCache<PatientDownloadTokenCacheItem, string> downloadTokenCache
) : HealthCareAppService, IPatientAppService
{
#region Get

    public virtual async Task<PatientDto> GetAsync(GetPatientInput input) =>
        ObjectMapper.Map<Patient, PatientDto>(await patientRepository.GetAsync(input.Id, input.PatientNo));

    public virtual async Task<PatientWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(
        GetPatientInput input
    ) =>
        ObjectMapper.Map<PatientWithNavigationProperties, PatientWithNavigationPropertiesDto>(
            await patientRepository.GetWithNavigationPropertiesAsync(input.Id, input.PatientNo)
        );

#endregion

#region GetList

    public virtual async Task<PagedResultDto<PatientDto>> GetListAsync(GetPatientsInput input)
    {
        var totalCount = await patientRepository.GetCountAsync(
            input.FilterText, input.No, input.CountryId, input.FullName, input.BirthDateMin,
            input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus
        );
        var items = await patientRepository.GetListAsync(
            input.FilterText, input.No, input.CountryId, input.FullName, input.BirthDateMin,
            input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.Sorting,
            input.MaxResultCount, input.SkipCount
        );

        return new PagedResultDto<PatientDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<Patient>, List<PatientDto>>(items)
        };
    }

    public virtual async Task<PagedResultDto<PatientWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(
        GetPatientsInput input
    )
    {
        var totalCount = await patientRepository.GetCountAsync(
            input.FilterText, input.No, input.CountryId, input.FullName, input.BirthDateMin,
            input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus
        );
        var items = await patientRepository.GetListWithNavigationPropertiesAsync(
            input.FilterText, input.No, input.CountryId, input.FullName, input.BirthDateMin,
            input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.Sorting,
            input.MaxResultCount, input.SkipCount
        );

        return new PagedResultDto<PatientWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper
                .Map<List<PatientWithNavigationProperties>, List<PatientWithNavigationPropertiesDto>>(items)
        };
    }

    public async Task<List<AddressDto>>
        GetPatientAddressesWithDetailsAsync(Guid patientId) =>
        ObjectMapper.Map<List<Address>, List<AddressDto>>(
            await addressRepository.GetListWithDetailsAsync(patientId)
        );

#endregion

#region Create

    [Authorize(HealthCarePermissions.Patients.Create)]
    public virtual async Task<PatientDto> CreateAsync(PatientCreateDto input)
    {
        await patientRules.EnsureIdentityNumberNotExistsAsync(input.IdentityNumber);
        await patientRules.EnsurePassportNumberNotExistsAsync(input.PassportNumber);

        var patient = await patientManager.CreateAsync(
            input.CountryId, input.PatientTypeId, input.InsuranceId, input.FirstName, input.LastName, input.BirthDate,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumberCode,
            input.MobilePhoneNumber, input.HomePhoneNumberCode, input.HomePhoneNumber, input.Gender, input.BloodType,
            input.MaritalStatus,
            ObjectMapper.Map<ICollection<AddressCreateDto>, ICollection<Address>>(input.Addresses),
            ObjectMapper.Map<ICollection<PatientNoteCreateDto>, ICollection<PatientNote>>(input.Notes)
        );
        return ObjectMapper.Map<Patient, PatientDto>(patient);
    }

#endregion

#region Update

    [Authorize(HealthCarePermissions.Patients.Edit)]
    public virtual async Task<PatientDto> UpdateAsync(Guid id, PatientUpdateDto input)
    {
        var patient = await patientManager.UpdateAsync(
            id, input.CountryId, input.PatientTypeId, input.InsuranceId, input.FirstName, input.LastName,
            input.BirthDate,
            input.EmailAddress, input.MobilePhoneNumberCode, input.MobilePhoneNumber, input.HomePhoneNumberCode,
            input.HomePhoneNumber, input.Gender, input.BloodType,
            input.MaritalStatus, ObjectMapper.Map<ICollection<AddressUpdateDto>, ICollection<Address>>(input.Addresses),
            ObjectMapper.Map<ICollection<PatientNoteUpdateDto>, ICollection<PatientNote>>(input.Notes),
            input.ConcurrencyStamp
        );
        return ObjectMapper.Map<Patient, PatientDto>(patient);
    }

#endregion

#region Delete

    [Authorize(HealthCarePermissions.Patients.Delete)]
    public virtual async Task DeleteAsync(Guid id) => await patientRepository.DeleteAsync(id);

    [Authorize(HealthCarePermissions.Patients.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> patientIds) =>
        await patientRepository.DeleteManyAsync(patientIds);

    [Authorize(HealthCarePermissions.Patients.Delete)]
    public virtual async Task DeleteAllAsync(GetPatientsInput input) =>
        await patientRepository.DeleteAllAsync(
            input.FilterText, input.No, input.CountryId, input.FullName, input.BirthDateMin,
            input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus
        );

#endregion

#region Excel

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientExcelDownloadDto input)
    {
        var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await patientRepository.GetListWithNavigationPropertiesAsync(
            input.FilterText, input.No, input.CountryId, input.FullName, input.BirthDateMin,
            input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus,
            $"{nameof(PatientExcelDto.No)} asc", 100
        );

        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(
            ObjectMapper.Map<List<PatientWithNavigationProperties>, List<PatientExcelDto>>(items)
        );
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new RemoteStreamContent(
            memoryStream,
            $"Patients_{DateTime.Now:dd.MM.yyyy hh:mm}.xlsx",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        );
    }

    public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new PatientDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) }
        );

        return new Shared.DownloadTokenResultDto { Token = token };
    }

#endregion
}