using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.RadiologyExaminations.Default)]
    public class RadiologyExaminationAppService(
        IRadiologyExaminationRepository radiologyExaminationRepository,
        RadiologyExaminationManager radiologyExaminationManager,
        IDistributedCache<RadiologyExaminationDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IRadiologyExaminationAppService
    {
        public virtual async Task<PagedResultDto<RadiologyExaminationDto>> GetListAsync(GetRadiologyExaminationsInput input)
        {
            var totalCount = await radiologyExaminationRepository.GetCountAsync(input.FilterText, input.Name, input.ExaminationCode, input.GroupId);
            var items = await radiologyExaminationRepository.GetListAsync(input.FilterText, input.Name, input.ExaminationCode, input.GroupId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RadiologyExaminationDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RadiologyExamination>, List<RadiologyExaminationDto>>(items)
            };
        }
        public virtual async Task<RadiologyExaminationDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<RadiologyExamination, RadiologyExaminationDto>(await radiologyExaminationRepository.GetAsync(id));
        }

        [Authorize(HealthCarePermissions.RadiologyExaminations.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await radiologyExaminationRepository.DeleteAsync(id);
        }
        public virtual async Task<RadiologyExaminationDto> CreateAsync(RadiologyExaminationCreateDto input)
        {
            var RadiologyExamination = await radiologyExaminationManager.CreateAsync(
                input.Name,
                input.ExaminationCode,
                input.GroupId
                );
            return ObjectMapper.Map<RadiologyExamination, RadiologyExaminationDto>(RadiologyExamination);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminations.Edit)]
        public virtual async Task<RadiologyExaminationDto> UpdateAsync(Guid id, RadiologyExaminationUpdateDto input)
        {
            var RadiologyExamination = await radiologyExaminationRepository.GetAsync(id);
            RadiologyExamination.Name = input.Name;
            RadiologyExamination.ExaminationCode = input.ExaminationCode;
            RadiologyExamination.GroupId = input.GroupId;

            await radiologyExaminationRepository.UpdateAsync(RadiologyExamination);
            return ObjectMapper.Map<RadiologyExamination, RadiologyExaminationDto>(RadiologyExamination);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await radiologyExaminationRepository.GetListAsync(input.FilterText, input.Name, input.ExaminationCode, input.GroupId);
            
            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<RadiologyExamination>, List<RadiologyExaminationDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HealthCarePermissions.RadiologyExaminations.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> RadiologyExaminationIds)
        {
            await radiologyExaminationRepository.DeleteManyAsync(RadiologyExaminationIds);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminations.Delete)]
        public virtual async Task DeleteAllAsync(GetRadiologyExaminationsInput input)
        {
            await radiologyExaminationRepository.DeleteAllAsync(input.FilterText, input.Name, input.ExaminationCode, input.GroupId);
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new RadiologyExaminationDownloadTokenCacheItem(),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)
                }
            );

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }

    }
}
