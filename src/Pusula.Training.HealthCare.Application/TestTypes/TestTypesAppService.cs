using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.TestTypes;
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

namespace Pusula.Training.HealthCare.TestTypes
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.TestTypes.Default)]
    public class TestTypesAppService(
        ITestTypeRepository testTypeRepository,
        TestTypeManager testTypeManager,
        IDistributedCache<TestTypeDownloadTokenCacheItem, string> downloadTokenCache,
        IDistributedEventBus distributedEventBus
    ) : HealthCareAppService, ITestTypesAppService
    {
        public IDistributedEventBus DistributedEventBus { get; } = distributedEventBus;

        #region Get

        public virtual async Task<TestTypeDto> GetAsync(Guid id)
        {
            var testType = await testTypeRepository.GetAsync(id);
            return ObjectMapper.Map<TestType, TestTypeDto>(testType);
        }

        #endregion

        #region GetList

        public virtual async Task<PagedResultDto<TestTypeDto>> GetListAsync(GetTestTypesInput input)
        {
            var totalCount = await testTypeRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await testTypeRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TestTypeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TestType>, List<TestTypeDto>>(items)
            };
        }

        #endregion

        #region Create

        [Authorize(HealthCarePermissions.TestTypes.Create)]
        public virtual async Task<TestTypeDto> CreateAsync(TestTypeCreateDto input)
        {
            var testType = await testTypeManager.CreateAsync(input.Name);
            return ObjectMapper.Map<TestType, TestTypeDto>(testType);
        }

        #endregion

        #region Update

        [Authorize(HealthCarePermissions.TestTypes.Edit)]
        public virtual async Task<TestTypeDto> UpdateAsync(Guid id, TestTypeUpdateDto input)
        {
            var testType = await testTypeManager.UpdateAsync(id, input.Name, input.ConcurrencyStamp);
            return ObjectMapper.Map<TestType, TestTypeDto>(testType);
        }

        #endregion

        #region Delete

        [Authorize(HealthCarePermissions.TestTypes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await testTypeRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.TestTypes.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> testTypeIds)
        {
            await testTypeRepository.DeleteManyAsync(testTypeIds);
        }

        [Authorize(HealthCarePermissions.TestTypes.Delete)]
        public virtual async Task DeleteAllAsync(GetTestTypesInput input)
        {
            await testTypeRepository.DeleteAllAsync(input.FilterText, input.Name);
        }

        #endregion

        #region Excel

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestTypeExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await testTypeRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<TestType>, List<TestTypeExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, $"TestTypes_{DateTime.Now:yyyy-MM-dd_HH-mm}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new TestTypeDownloadTokenCacheItem { Token = token },
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
