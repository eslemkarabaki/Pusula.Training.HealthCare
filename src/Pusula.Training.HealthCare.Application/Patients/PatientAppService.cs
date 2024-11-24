using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.PatientTypes;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.Patients;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Patients.Default)]
public class PatientAppService(
    IPatientRepository patientRepository,
    IAddressRepository addressRepository,
    PatientManager patientManager,
    IDistributedCache<PatientDownloadTokenCacheItem, string> downloadTokenCache,
    IDistributedEventBus distributedEventBus
) : HealthCareAppService, IPatientAppService
{
    #region Get

    public virtual async Task<PatientDto> GetAsync(string number)
    {
        var patient = await patientRepository.GetAsync(e=>e.IdentityNumber== number || e.PassportNumber== number);
        return ObjectMapper.Map<Patient, PatientDto>(patient);
    }
    public virtual async Task<PatientDto> GetAsync(Guid id)
    {
        var patient = await patientRepository.GetAsync(id);
        return ObjectMapper.Map<Patient, PatientDto>(patient);
    }

    public virtual async Task<PatientWithNavigationPropertiesDto> GetNavigationPropertiesAsync(Guid id)
    {
        var patient = await patientRepository.GetNavigationPropertiesAsync(id);
        return ObjectMapper.Map<PatientWithNavigationProperties, PatientWithNavigationPropertiesDto>(patient);
    }

#endregion

#region GetList

    public virtual async Task<PagedResultDto<PatientDto>> GetListAsync(GetPatientsInput input)
    {
        var totalCount = await patientRepository.GetCountAsync(
            input.FilterText, input.CountryId, input.FirstName, input.LastName, input.BirthDateMin, input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus
        );
        var items = await patientRepository.GetListAsync(
            input.FilterText, input.CountryId, input.FirstName, input.LastName, input.BirthDateMin, input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.Sorting,
            input.MaxResultCount, input.SkipCount
        );

        return new PagedResultDto<PatientDto>
        {
            TotalCount = totalCount, Items = ObjectMapper.Map<List<Patient>, List<PatientDto>>(items)
        };
    }

    public virtual async Task<PagedResultDto<PatientWithNavigationPropertiesDto>> GetNavigationPropertiesListAsync(
        GetPatientsInput input
    )
    {
        var totalCount = await patientRepository.GetCountAsync(
            input.FilterText, input.CountryId, input.FirstName, input.LastName, input.BirthDateMin, input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus
        );
        var items = await patientRepository.GetNavigationPropertiesListAsync(
            input.FilterText, input.CountryId, input.FirstName, input.LastName, input.BirthDateMin, input.BirthDateMax,
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

    public async Task<List<AddressWithNavigationPropertiesDto>>
        GetAddressNavigationPropertiesListAsync(Guid patientId) =>
        ObjectMapper.Map<List<AddressWithNavigationProperties>, List<AddressWithNavigationPropertiesDto>>(
            await addressRepository.GetNavigationPropertiesListAsync(patientId)
        );

#endregion

#region Create

    [Authorize(HealthCarePermissions.Patients.Create)]
    public virtual async Task<PatientDto> CreateAsync(PatientCreateDto input)
    {
        var patient = await patientManager.CreateAsync(
            input.CountryId, input.PatientTypeId, input.FirstName, input.LastName, input.BirthDate,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumberCode,
            input.MobilePhoneNumber, input.HomePhoneNumberCode, input.HomePhoneNumber, input.Gender, input.BloodType,
            input.MaritalStatus,
            ObjectMapper.Map<IEnumerable<AddressCreateDto>, IEnumerable<Address>>(input.Addresses)
        );
        return ObjectMapper.Map<Patient, PatientDto>(patient);
    }

#endregion

#region Update

    [Authorize(HealthCarePermissions.Patients.Edit)]
    public virtual async Task<PatientDto> UpdateAsync(Guid id, PatientUpdateDto input)
    {
        var patient = await patientManager.UpdateAsync(
            id, input.CountryId, input.PatientTypeId, input.FirstName, input.LastName, input.BirthDate,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumberCode,
            input.MobilePhoneNumber, input.HomePhoneNumberCode, input.HomePhoneNumber, input.Gender, input.BloodType,
            input.MaritalStatus, ObjectMapper.Map<IEnumerable<AddressUpdateDto>, IEnumerable<Address>>(input.Addresses),
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
            input.FilterText, input.CountryId, input.FirstName, input.LastName, input.BirthDateMin, input.BirthDateMax,
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

        var items = await patientRepository.GetNavigationPropertiesListAsync(
            input.FilterText, input.CountryId, input.FirstName, input.LastName, input.BirthDateMin, input.BirthDateMax,
            input.IdentityNumber, input.PassportNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus
        );

        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(
            ObjectMapper.Map<List<PatientWithNavigationProperties>, List<PatientExcelDto>>(items)
        );
        memoryStream.Seek(0, SeekOrigin.Begin);

        //todo excel name
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