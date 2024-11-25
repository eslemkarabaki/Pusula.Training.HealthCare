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

namespace Pusula.Training.HealthCare.TestGroups
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.TestGroups.Default)]
    public class TestGroupsAppService(
        ITestGroupRepository testGroupRepository,
        TestGroupManager testGroupManager,
        IDistributedCache<TestGroupDownloadTokenCacheItem, string> downloadTokenCache,
        IDistributedEventBus distributedEventBus
    ) : HealthCareAppService, ITestGroupsAppService
    {
        public IDistributedEventBus DistributedEventBus { get; } = distributedEventBus;

        #region Get

        public virtual async Task<TestGroupDto> GetAsync(Guid id)
        {
            var testGroup = await testGroupRepository.GetAsync(id);
            return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
        }

        #endregion

        #region GetList

        public virtual async Task<PagedResultDto<TestGroupDto>> GetListAsync(GetTestGroupsInput input)
        {
            var totalCount = await testGroupRepository.GetCountAsync(input.FilterText, input.Code, input.Name);
            var items = await testGroupRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TestGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TestGroup>, List<TestGroupDto>>(items)
            };
        }

        #endregion

        #region Create

        [Authorize(HealthCarePermissions.TestGroups.Create)]
        public virtual async Task<TestGroupDto> CreateAsync(TestGroupCreateDto input)
        {
            var testGroup = await testGroupManager.CreateAsync(input.Code, input.Name);
            return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
        }

        #endregion

        #region Update

        [Authorize(HealthCarePermissions.TestGroups.Edit)]
        public virtual async Task<TestGroupDto> UpdateAsync(Guid id, TestGroupUpdateDto input)
        {
            var testGroup = await testGroupManager.UpdateAsync(id, input.Code, input.Name, input.ConcurrencyStamp);
            return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
        }

        #endregion

        #region Delete

        [Authorize(HealthCarePermissions.TestGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await testGroupRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.TestGroups.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> testGroupIds)
        {
            await testGroupRepository.DeleteManyAsync(testGroupIds);
        }

        [Authorize(HealthCarePermissions.TestGroups.Delete)]
        public virtual async Task DeleteAllAsync(GetTestGroupsInput input)
        {
            await testGroupRepository.DeleteAllAsync(input.FilterText, input.Code, input.Name);
        }

        #endregion

        #region Excel

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestGroupExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await testGroupRepository.GetListAsync(input.FilterText, input.Code, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<TestGroup>, List<TestGroupExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, $"TestGroups_{DateTime.Now:yyyy-MM-dd_HH-mm}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new TestGroupDownloadTokenCacheItem { Token = token },
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
