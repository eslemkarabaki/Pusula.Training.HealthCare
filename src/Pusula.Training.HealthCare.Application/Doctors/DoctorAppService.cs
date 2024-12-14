using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.GlobalExceptions;
using Pusula.Training.HealthCare.Users;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Doctors.Default)]
public class DoctorAppService(
    IDoctorRepository doctorRepository,
    DoctorManager doctorManager,
    IdentityUserManager userManager,
    IDistributedCache<DoctorDownloadTokenCacheItem, string> downloadTokenCache,
    ILogger<DoctorAppService> logger,
    IUserRules userRules
) : HealthCareAppService, IDoctorAppService
{
    //For Appointment
    public async Task<List<DoctorDto>> GetListDoctorsAsync() =>
        ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(await doctorRepository.GetListAsync());

    // For Appointment
    public async Task<List<DoctorDto>> GetListDoctorsAsync(Guid id) =>
        ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(await doctorRepository.GetListAsync(departmentId: id));

    public virtual async Task<PagedResultDto<DoctorDto>> GetListAsync(GetDoctorsInput input)
    {
        var totalCount = await doctorRepository.GetCountAsync(
            input.FilterText, input.FullName, input.AppointmentTime, input.TitleId,
            input.DepartmentId, input.HospitalId
        );
        var items = await doctorRepository.GetListAsync(
            input.FilterText, input.FullName, input.AppointmentTime, input.TitleId,
            input.DepartmentId, input.HospitalId, input.Sorting, input.MaxResultCount, input.SkipCount
        );

        return new PagedResultDto<DoctorDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(items)
        };
    }

    public virtual async Task<PagedResultDto<DoctorWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(
        GetDoctorsInput input
    )
    {
        var totalCount = await doctorRepository.GetCountAsync(
            input.FilterText, input.FullName, input.AppointmentTime, input.TitleId,
            input.DepartmentId, input.HospitalId
        );
        var items = await doctorRepository.GetListWithNavigationPropertiesAsync(
            input.FilterText, input.FullName, input.AppointmentTime, input.TitleId,
            input.DepartmentId, input.HospitalId, input.Sorting, input.MaxResultCount, input.SkipCount
        );

        return new PagedResultDto<DoctorWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<DoctorWithNavigationProperties>, List<DoctorWithNavigationPropertiesDto>>(
                items
            )
        };
    }

    public virtual async Task<DoctorDto> GetAsync(Guid id)
    {
        var doctor = await doctorRepository.GetAsync(id);
        return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
    }

    public async Task<IdentityUserDto?> GetDoctorUserAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        GlobalException.ThrowIf(user == null, "Doctor user not found");
        return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user!);
    }

    [Authorize(HealthCarePermissions.Doctors.Delete)]
    public virtual async Task DeleteAsync(Guid id) => await doctorRepository.DeleteAsync(id);

    public async Task<DoctorDto> GetDoctorAsync(Guid id)
    {
        var doctor = await doctorRepository.GetAsync(id);
        return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
    }

    [Authorize(HealthCarePermissions.Doctors.Create)]
    public virtual async Task<DoctorDto> CreateAsync(DoctorCreateDto input)
    {
        await userRules.EnsureUsernameNotExistAsync(input.User.UserName);
        await userRules.EnsureEmailNotExistAsync(input.User.Email);

        var doctor = await doctorManager.CreateAsync(
            input.FirstName,
            input.LastName,
            input.AppointmentTime,
            input.TitleId!.Value,
            input.DepartmentId!.Value,
            input.HospitalId!.Value,
            input.User.UserName,
            input.User.Email,
            input.User.Password
        );
        return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
    }

    [Authorize(HealthCarePermissions.Doctors.Edit)]
    public virtual async Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input)
    {
        var doctor = await doctorManager.UpdateAsync(
            id, input.FirstName, input.LastName, input.AppointmentTime, input.TitleId!.Value, input.DepartmentId!.Value,
            input.ConcurrencyStamp
        );
        return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
    }

    [Authorize(HealthCarePermissions.Doctors.Edit)]
    public async Task<bool> ChangePasswordAsync(Guid userId, DoctorUserPasswordUpdateDto input)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        GlobalException.ThrowIf(
            !await userManager.CheckPasswordAsync(user!, input.CurrentPassword), "Current password is incorrect."
        );

        var result = await userManager.ChangePasswordAsync(user!, input.CurrentPassword, input.NewPassword);
        GlobalException.ThrowIf(!result.Succeeded, result.Errors.Select(e => e.Description));
        return result.Succeeded;
    }

    [Authorize(HealthCarePermissions.Doctors.Edit)]
    public async Task<bool> UpdateUserAsync(Guid userId, DoctorUserInformationUpdateDto input)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        await userRules.EnsureUsernameNotExistForOthersAsync(input.UserName, userId);
        await userRules.EnsureEmailNotExistForOthersAsync(input.Email, userId);

        var result = await userManager.SetUserNameAsync(user!, input.UserName);
        GlobalException.ThrowIf(!result.Succeeded, result.Errors.Select(e => e.Description));

        result = await userManager.SetEmailAsync(user!, input.Email);
        GlobalException.ThrowIf(!result.Succeeded, result.Errors.Select(e => e.Description));

        return result.Succeeded;
    }

    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input)
    {
        var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
        GlobalException.ThrowIf(downloadToken is null, "Invalid download token: " + input.DownloadToken);

        var items = await doctorRepository.GetListAsync(
            input.FilterText, input.FullName, input.AppointmentTime, input.TitleId,
            input.DepartmentId, input.HospitalId
        );

        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Doctor>, List<DoctorExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new RemoteStreamContent(
            memoryStream, "Doctors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        );
    }

    [Authorize(HealthCarePermissions.Doctors.Delete)]
    public async Task DeleteByIdsAsync(List<Guid> doctorIds) => await doctorRepository.DeleteManyAsync(doctorIds);

    [Authorize(HealthCarePermissions.Doctors.Delete)]
    public virtual async Task DeleteAllAsync(GetDoctorsInput input) =>
        await doctorRepository.DeleteAllAsync(
            input.FilterText, input.FullName, input.AppointmentTime, input.TitleId, input.DepartmentId
        );

    public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new DoctorDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) }
        );

        return new DownloadTokenResultDto { Token = token };
    }
}