using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.TestProcesses
{
    [RemoteService]
    [Area("app")]
    [ControllerName("TestProcess")]
    [Route("api/app/test-processes")]
    public class TestProcessesController : HealthCareController, ITestProcessesAppService
    {
        private readonly ITestProcessesAppService _testProcessesAppService;

        public TestProcessesController(ITestProcessesAppService testProcessesAppService)
        {
            _testProcessesAppService = testProcessesAppService;
        }

        [HttpPost]
        public virtual Task<TestProcessDto> CreateAsync(TestProcessCreateDto input)
        {
            return _testProcessesAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetTestProcessesInput input)
        {
            return _testProcessesAppService.DeleteAllAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _testProcessesAppService.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> ids)
        {
            return _testProcessesAppService.DeleteByIdsAsync(ids);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TestProcessDto> GetAsync(Guid id)
        {
            return _testProcessesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _testProcessesAppService.GetDownloadTokenAsync();
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestProcessExcelDownloadDto input)
        {
            return _testProcessesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input)
        {
            return _testProcessesAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TestProcessDto> UpdateAsync(Guid id, TestProcessUpdateDto input)
        {
            return _testProcessesAppService.UpdateAsync(id, input);
        }
    }
}
