using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Tests;
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

namespace Pusula.Training.HealthCare.Tests
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Tests.Default)]
    public class TestsAppService(
        ITestRepository testRepository,
        TestManager testManager,
        IDistributedCache<TestDownloadTokenCacheItem, string> downloadTokenCache,
        IDistributedEventBus distributedEventBus
    ) : HealthCareAppService, ITestsAppService
    {
        public IDistributedEventBus DistributedEventBus { get; } = distributedEventBus;
        #region Get

        public virtual async Task<TestDto> GetAsync(Guid id)
        {
            var test = await testRepository.GetAsync(id);
            return ObjectMapper.Map<Test, TestDto>(test);
        }

        #endregion

        #region GetList

        public virtual async Task<PagedResultDto<TestDto>> GetListAsync(GetTestsInput input)
        {
            var totalCount = await testRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.TestGroupId);
            var items = await testRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.TestGroupId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TestDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Test>, List<TestDto>>(items)
            };
        }

        #endregion

        #region Create

        [Authorize(HealthCarePermissions.Tests.Create)]
        public virtual async Task<TestDto> CreateAsync(TestCreateDto input)
        {
            var test = await testManager.CreateAsync(input.Code, input.Name, input.TestGroupId);
            return ObjectMapper.Map<Test, TestDto>(test);
        }

        #endregion

        #region Update

        [Authorize(HealthCarePermissions.Tests.Edit)]
        public virtual async Task<TestDto> UpdateAsync(Guid id, TestUpdateDto input)
        {
            var test = await testManager.UpdateAsync(id, input.Code, input.Name, input.TestGroupId, input.ConcurrencyStamp);
            return ObjectMapper.Map<Test, TestDto>(test);
        }

        #endregion

        #region Delete

        [Authorize(HealthCarePermissions.Tests.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await testRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.Tests.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> testIds)
        {
            await testRepository.DeleteManyAsync(testIds);
        }

        [Authorize(HealthCarePermissions.Tests.Delete)]
        public virtual async Task DeleteAllAsync(GetTestsInput input)
        {
            await testRepository.DeleteAllAsync(input.FilterText, input.Code, input.Name, input.TestGroupId);
        }

        #endregion

        #region Excel

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await testRepository.GetListAsync(input.FilterText, input.Code, input.Name, input.TestGroupId);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Test>, List<TestExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, $"Tests_{DateTime.Now:yyyy-MM-dd_HH-mm}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new TestDownloadTokenCacheItem { Token = token },
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
