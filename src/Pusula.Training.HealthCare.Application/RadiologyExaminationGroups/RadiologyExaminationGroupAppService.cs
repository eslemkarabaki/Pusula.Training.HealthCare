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

namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.RadiologyExaminationGroups.Default)]
    public class RadiologyExaminationGroupAppService(
        IRadiologyExaminationGroupRepository radiologyExaminationGroupRepository,
        RadiologyExaminationGroupManager radiologyExaminationGroupManager, 
        IDistributedCache<RadiologyExaminationGroupDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IRadiologyExaminationGroupAppService
    {
        public virtual async Task<PagedResultDto<RadiologyExaminationGroupDto>> GetListAsync(GetRadiologyExaminationGroupsInput input)
        {
            var totalCount = await radiologyExaminationGroupRepository.GetCountAsync(input.FilterText, input.Name, input.Description);
            var items = await radiologyExaminationGroupRepository.GetListAsync(input.FilterText, input.Name, input.Description, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RadiologyExaminationGroupDto> 
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RadiologyExaminationGroup>, List<RadiologyExaminationGroupDto>>(items)
            };
        }

        public virtual async Task<RadiologyExaminationGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<RadiologyExaminationGroup, RadiologyExaminationGroupDto>(await radiologyExaminationGroupRepository.GetAsync(id));
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await radiologyExaminationGroupRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationGroups.Create)]
        public virtual async Task<RadiologyExaminationGroupDto> CreateAsync(RadiologyExaminationGroupCreateDto input)
        {
            try
            {
                var radiologyExaminationGroup = await radiologyExaminationGroupManager.CreateAsync(
                    input.Name,
                    input.Description ?? string.Empty 
                );
                return ObjectMapper.Map<RadiologyExaminationGroup, RadiologyExaminationGroupDto>(radiologyExaminationGroup);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("An error occurred while creating the radiology examination group. Please try again.", ex.Message);
            }
        }


        [Authorize(HealthCarePermissions.RadiologyExaminationGroups.Edit)]
        public virtual async Task<RadiologyExaminationGroupDto> UpdateAsync(Guid id, RadiologyExaminationGroupUpdateDto input)
        {
            var radiologyExaminationGroup = await radiologyExaminationGroupManager.UpdateAsync(
                id,
                input.Name,
                input.Description,
                input.ConcurrencyStamp
            );
            return ObjectMapper.Map<RadiologyExaminationGroup, RadiologyExaminationGroupDto>(radiologyExaminationGroup);
        }


        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationGroupExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await radiologyExaminationGroupRepository.GetListAsync(input.FilterText, input.Name, input.Description);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<RadiologyExaminationGroup>, List<RadiologyExaminationGroupDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "RadiologyExaminationGroup.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationGroups.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> RadiologyExaminationGroupIds)
        {
            await radiologyExaminationGroupRepository.DeleteManyAsync(RadiologyExaminationGroupIds);
        }

        [Authorize(HealthCarePermissions.RadiologyExaminationGroups.Delete)]
        public virtual async Task DeleteAllAsync(GetRadiologyExaminationGroupsInput input)
        {
            await radiologyExaminationGroupRepository.DeleteAllAsync(input.FilterText, input.Name, input.Description);
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new RadiologyExaminationGroupDownloadTokenCacheItem(),
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
