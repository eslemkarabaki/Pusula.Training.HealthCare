using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.TestProcesses
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.TestProcesses.Default)]
    public class TestProcessesAppService : HealthCareAppService, ITestProcessesAppService
    {
        private readonly IRepository<TestProcess, Guid> _testProcessRepository;

        public TestProcessesAppService(IRepository<TestProcess, Guid> testProcessRepository)
        {
            _testProcessRepository = testProcessRepository;
        }

        public async Task<TestProcessDto> GetAsync(Guid id)
        {
            var testProcess = await _testProcessRepository.GetAsync(id);
            return ObjectMapper.Map<TestProcess, TestProcessDto>(testProcess);
        }

        public async Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input)
        {
            var totalCount = await _testProcessRepository.GetCountAsync();
            var items = await _testProcessRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<TestProcessDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TestProcess>, List<TestProcessDto>>(items)
            };
        }

        [Authorize(HealthCarePermissions.TestProcesses.Create)]
        public async Task<TestProcessDto> CreateAsync(TestProcessCreateDto input)
        {
            var testProcess = ObjectMapper.Map<TestProcessCreateDto, TestProcess>(input);
            await _testProcessRepository.InsertAsync(testProcess);
            return ObjectMapper.Map<TestProcess, TestProcessDto>(testProcess);
        }

        [Authorize(HealthCarePermissions.TestProcesses.Edit)]
        public async Task<TestProcessDto> UpdateAsync(Guid id, TestProcessUpdateDto input)
        {
            var testProcess = await _testProcessRepository.GetAsync(id);
            ObjectMapper.Map(input, testProcess);
            await _testProcessRepository.UpdateAsync(testProcess);
            return ObjectMapper.Map<TestProcess, TestProcessDto>(testProcess);
        }

        [Authorize(HealthCarePermissions.TestProcesses.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _testProcessRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.TestProcesses.Delete)]
        public async Task DeleteByIdsAsync(List<Guid> ids)
        {
            await _testProcessRepository.DeleteManyAsync(ids);
        }

        [AllowAnonymous]
        public async Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestProcessExcelDownloadDto input)
        {
            var items = await _testProcessRepository.GetListAsync();
            var memoryStream = new MemoryStream();
            // Convert items to Excel format here...

            return new RemoteStreamContent(memoryStream, "TestProcesses.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");
            return new DownloadTokenResultDto { Token = token };
        }

        public Task DeleteAllAsync(GetTestProcessesInput input)
        {
            throw new NotImplementedException();
        }
    }
}
