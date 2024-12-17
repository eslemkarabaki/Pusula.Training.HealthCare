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
namespace Pusula.Training.HealthCare.Examinations;/*
[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Examinations.Default)]
public class ExaminationAppService(
    IExaminationRepository examinationRepository,
    //ExaminationManager examinationManager,
    IDistributedCache<ExaminationDownloadTokenCacheItem, string> downloadTokenCache
    ) : HealthCareAppService, IExaminationAppService
{
    public virtual async Task<ExaminationDto> GetAsync(Guid id)
    {
        var examination = await examinationRepository.GetAsync(id);
        return ObjectMapper.Map<Examination, ExaminationDto>(examination);
    }
    public virtual async Task<PagedResultDto<ExaminationDto>> GetListAsync(GetExaminationsInput input)
    {
        var totalCount = await examinationRepository.GetCountAsync(input.FilterText, input.Notes, input.ChronicDiseases, input.Allergies, input.VisitDate, input.IdentityNumber, input.Medications, input.Diagnosis,
            input.Prescription,input.ImagingResults, input.PatientId, input.DoctorId);
        var items = await examinationRepository.GetListAsync(input.FilterText, input.Notes, input.ChronicDiseases, input.Allergies, input.VisitDate, input.IdentityNumber, input.Medications, input.Diagnosis,
            input.Prescription,input.ImagingResults, input.PatientId, input.DoctorId, input.Sorting,
            input.MaxResultCount,
            input.SkipCount);
        return new PagedResultDto<ExaminationDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<Examination>, List<ExaminationDto>>(items)
        };
    }
    [Authorize(HealthCarePermissions.Examinations.Create)]
    public virtual async Task<ExaminationDto> CreateAsync(ExaminationCreateDto input)
    {
        var examination = await examinationManager.CreateAsync(input.DoctorId, input.PatientId, input.Notes,
            input.ChronicDiseases, input.VisitDate, input.IdentityNumber, input.Allergies, input.Medications,input.Diagnosis,
            input.Prescription, input.ImagingResults);
        return ObjectMapper.Map<Examination, ExaminationDto>(examination);
}
[Authorize(HealthCarePermissions.Examinations.Edit)]
    public virtual async Task<ExaminationDto> UpdateAsync(Guid id, ExaminationUpdateDto input)
    {
        var examination = await examinationManager.UpdateAsync(
            id,
            input.DoctorId, input.PatientId, input.ChronicDiseases,
            input.Allergies, input.Medications, input.Diagnosis, input.Prescription, input.VisitDate,
            input.IdentityNumber, input.Notes, input.ImagingResults, input.ConcurrencyStamp
        );
        return ObjectMapper.Map<Examination, ExaminationDto>(examination);
    }
    [Authorize(HealthCarePermissions.Examinations.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await examinationRepository.DeleteAsync(id);
    }
    [Authorize(HealthCarePermissions.Examinations.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> examinationIds)
    {
        await examinationRepository.DeleteManyAsync(examinationIds);
    }
    [Authorize(HealthCarePermissions.Examinations.Delete)]
    public virtual async Task DeleteAllAsync(GetExaminationsInput input)
    {
        await examinationRepository.DeleteAllAsync();
    }
    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ExaminationExcelDownloadDto input)
    {
        var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }
        var items = await examinationRepository.GetListAsync(input.FilterText, input.Notes, input.ChronicDiseases, input.Allergies,
            input.VisitDate, input.IdentityNumber, input.Medications, input.Diagnosis, input.Prescription, input.ImagingResults, input.PatientId, input.DoctorId);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Examination>, List<ExaminationExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        //todo excel name
        return new RemoteStreamContent(memoryStream, "Examinations.xlsx",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }
    public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await downloadTokenCache.SetAsync(
            token,
            new ExaminationDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });
        return new Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}
    */