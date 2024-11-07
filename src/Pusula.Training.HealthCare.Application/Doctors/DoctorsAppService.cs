using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.EventBus.Distributed;

namespace Pusula.Training.HealthCare.Doctors;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Doctors.Default)]
public class DoctorsAppService(
    IDoctorRepository DoctorRepository,
    DoctorManager DoctorManager,
    IDistributedCache<DoctorDownloadTokenCacheItem, string> downloadTokenCache,
    IDistributedEventBus distributedEventBus) : HealthCareAppService, IDoctorsAppService
{
    #region Get

    public virtual async Task<DoctorDto> GetAsync(Guid id)
    {
        var Doctor = await DoctorRepository.GetAsync(id);
        return ObjectMapper.Map<Doctor, DoctorDto>(Doctor);
    }

    public virtual async Task<DoctorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<DoctorWithNavigationProperties, DoctorWithNavigationPropertiesDto>(
            await DoctorRepository.GetWithNavigationPropertiesAsync(id));
    }

    #endregion

    #region GetList

    public virtual async Task<PagedResultDto<DoctorDto>> GetListAsync(GetDoctorsInput input)
    {
        var totalCount = await DoctorRepository.GetCountAsync(input.FilterText, input.FirstName, input.LastName,
            input.BirthDateMin, input.BirthDateMax, input.IdentityNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.CountryId);
        var items = await DoctorRepository.GetListAsync(input.FilterText, input.FirstName, input.LastName,
            input.BirthDateMin, input.BirthDateMax, input.IdentityNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.CountryId, input.Sorting,
            input.MaxResultCount,
            input.SkipCount);

        return new PagedResultDto<DoctorDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(items)
        };
    }

    public virtual async Task<PagedResultDto<DoctorWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(
        GetDoctorsInput input)
    {
        var totalCount = await DoctorRepository.GetCountAsync(input.FilterText, input.FirstName, input.LastName,
            input.BirthDateMin, input.BirthDateMax, input.IdentityNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.CountryId);
        var items = await DoctorRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.FirstName,
            input.LastName,
            input.BirthDateMin, input.BirthDateMax, input.IdentityNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.CountryId, input.Sorting,
            input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<DoctorWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items =
                ObjectMapper.Map<List<DoctorWithNavigationProperties>, List<DoctorWithNavigationPropertiesDto>>(items)
        };
    }

    #endregion

    #region Create

    [Authorize(HealthCarePermissions.Doctors.Create)]
    public virtual async Task<DoctorDto> CreateAsync(DoctorCreateDto input)
    {
        var Doctor = await DoctorManager.CreateAsync(input.CountryId, input.FirstName, input.LastName,
            input.BirthDate, input.IdentityNumber, input.EmailAddress, input.MobilePhoneNumber, input.Gender,
            input.BloodType, input.MaritalStatus, input.HomePhoneNumber);
        return ObjectMapper.Map<Doctor, DoctorDto>(Doctor);
    }

    #endregion

    #region Update

    [Authorize(HealthCarePermissions.Doctors.Edit)]
    public virtual async Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input)
    {
        var Doctor = await DoctorManager.UpdateAsync(
            id,
            input.CountryId, input.FirstName, input.LastName,
            input.BirthDate, input.IdentityNumber, input.EmailAddress, input.MobilePhoneNumber, input.Gender,
            input.BloodType, input.MaritalStatus, input.HomePhoneNumber, input.ConcurrencyStamp
        );
        return ObjectMapper.Map<Doctor, DoctorDto>(Doctor);
    }

    #endregion

    #region Delete

    [Authorize(HealthCarePermissions.Doctors.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await DoctorRepository.DeleteAsync(id);
    }

    [Authorize(HealthCarePermissions.Doctors.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> DoctorIds)
    {
        await DoctorRepository.DeleteManyAsync(DoctorIds);
    }

    [Authorize(HealthCarePermissions.Doctors.Delete)]
    public virtual async Task DeleteAllAsync(GetDoctorsInput input)
    {
        await DoctorRepository.DeleteAllAsync(input.FilterText, input.FirstName, input.LastName,
            input.BirthDateMin, input.BirthDateMax, input.IdentityNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.CountryId);
    }

    #endregion

    #region Excel

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input)
    {
        var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await DoctorRepository.GetListAsync(input.FilterText, input.FirstName, input.LastName,
            input.BirthDateMin, input.BirthDateMax, input.IdentityNumber, input.EmailAddress, input.MobilePhoneNumber,
            input.HomePhoneNumber, input.Gender, input.BloodType, input.MaritalStatus, input.CountryId);

        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Doctor>, List<DoctorExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);

        //todo excel name
        return new RemoteStreamContent(memoryStream, "Doctors.xlsx",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new DoctorDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

        return new Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }

    #endregion
}