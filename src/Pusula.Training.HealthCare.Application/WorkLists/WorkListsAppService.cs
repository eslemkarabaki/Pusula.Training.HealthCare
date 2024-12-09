using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Laboratory;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.WorkLists;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.WorkLists
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.WorkLists.Default)]
    public class WorkListsAppService(
    IWorkListRepository WorkListRepository,
        WorkListManager WorkListManager,
        IDistributedCache<WorkListDownloadTokenCacheItem, string> downloadTokenCache
    ) : HealthCareAppService, IWorkListsAppService
    {
        #region Get

        public virtual async Task<WorkListDto> GetAsync(Guid id)
        {
            var WorkList = await WorkListRepository.GetAsync(id);
            return ObjectMapper.Map<WorkList, WorkListDto>(WorkList);
        }

        #endregion

        #region GetList

        public virtual async Task<PagedResultDto<WorkListDto>> GetListAsync(GetWorkListsInput input)
        {
            var totalCount = await WorkListRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.DepartmentId);
            var items = await WorkListRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.DepartmentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<WorkListDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WorkList>, List<WorkListDto>>(items)
            };
        }

        #endregion

        #region Create

        [Authorize(HealthCarePermissions.WorkLists.Create)]
        public virtual async Task<WorkListDto> CreateAsync(WorkListCreateDto input)
        {
            var WorkList = await WorkListManager.CreateAsync(input.Code, input.Name, input.DepartmentId);
            return ObjectMapper.Map<WorkList, WorkListDto>(WorkList);
        }

        #endregion

        #region Update

        [Authorize(HealthCarePermissions.WorkLists.Edit)]
        public virtual async Task<WorkListDto> UpdateAsync(Guid id, WorkListUpdateDto input)
        {
            var WorkList = await WorkListManager.UpdateAsync(
                id,
                input.Code,
                input.Name,
            input.DepartmentId,
                input.ConcurrencyStamp
            );
            return ObjectMapper.Map<WorkList, WorkListDto>(WorkList);
        }

        #endregion

        #region Delete

        [Authorize(HealthCarePermissions.WorkLists.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await WorkListRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.WorkLists.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> WorkListIds)
        {
            await WorkListRepository.DeleteManyAsync(WorkListIds);
        }

        [Authorize(HealthCarePermissions.WorkLists.Delete)]
        public virtual async Task DeleteAllAsync(GetWorkListsInput input)
        {
            await WorkListRepository.DeleteAllAsync(input.FilterText, input.Code, input.Name, input.DepartmentId);
        }

        #endregion

        #region Excel

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(WorkListExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await WorkListRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.DepartmentId);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<WorkList>, List<WorkListExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(
                memoryStream,
                $"WorkLists_{DateTime.Now:yyyy-MM-dd_HH-mm}.xlsx",
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            );
        }

        public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");
            await downloadTokenCache.SetAsync(
                token,
                new WorkListDownloadTokenCacheItem { Token = token },
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
}
