using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestGroups
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.TestGroups.Default)]
    public class TestGroupsAppService : HealthCareAppService, ITestGroupsAppService
    {
        private readonly IRepository<TestGroup, Guid> _testGroupRepository;

        public TestGroupsAppService(IRepository<TestGroup, Guid> testGroupRepository)
        {
            _testGroupRepository = testGroupRepository;
        }

        public async Task<TestGroupDto> GetAsync(Guid id)
        {
            var testGroup = await _testGroupRepository.GetAsync(id);
            return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
        }

        public async Task<PagedResultDto<TestGroupDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var totalCount = await _testGroupRepository.GetCountAsync();
            var items = await _testGroupRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<TestGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TestGroup>, List<TestGroupDto>>(items)
            };
        }

        [Authorize(HealthCarePermissions.TestGroups.Create)]
        public async Task<TestGroupDto> CreateAsync(TestGroupCreateDto input)
        {
            var testGroup = ObjectMapper.Map<TestGroupCreateDto, TestGroup>(input);
            await _testGroupRepository.InsertAsync(testGroup);
            return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
        }

        [Authorize(HealthCarePermissions.TestGroups.Edit)]
        public async Task<TestGroupDto> UpdateAsync(Guid id, TestGroupUpdateDto input)
        {
            var testGroup = await _testGroupRepository.GetAsync(id);
            ObjectMapper.Map(input, testGroup);
            await _testGroupRepository.UpdateAsync(testGroup);
            return ObjectMapper.Map<TestGroup, TestGroupDto>(testGroup);
        }

        [Authorize(HealthCarePermissions.TestGroups.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _testGroupRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.TestGroups.Delete)]
        public async Task DeleteByIdsAsync(List<Guid> ids)
        {
            await _testGroupRepository.DeleteManyAsync(ids);
        }

        public Task<PagedResultDto<TestGroupDto>> GetListAsync(GetTestGroupsInput input)
        {
            throw new NotImplementedException();
        }

        public Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestGroupExcelDownloadDto input)
        {
            throw new NotImplementedException();
        }

        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
