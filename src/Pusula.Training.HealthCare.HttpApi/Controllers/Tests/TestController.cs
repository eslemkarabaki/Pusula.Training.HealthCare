using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.Tests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Tests;

[RemoteService]
[Area("app")]
[ControllerName("Test")]
[Route("api/app/tests")]
public class TestController(ITestsAppService testsAppService) : HealthCareController, ITestsAppService
{
    [HttpPost]
    public virtual Task<TestDto> CreateAsync(TestCreateDto input)
    {
        return testsAppService.CreateAsync(input);
    }

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetTestsInput input)
    {
        return testsAppService.DeleteAllAsync(input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return testsAppService.DeleteAsync(id);
    }

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> testIds)
    {
        return testsAppService.DeleteByIdsAsync(testIds);
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<TestDto> GetAsync(Guid id)
    {
        return testsAppService.GetAsync(id);
    }

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return testsAppService.GetDownloadTokenAsync();
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestExcelDownloadDto input)
    {
        return testsAppService.GetListAsExcelFileAsync(input);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<TestDto>> GetListAsync(GetTestsInput input)
    {
        return testsAppService.GetListAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<TestDto> UpdateAsync(Guid id, TestUpdateDto input)
    {
        return testsAppService.UpdateAsync(id, input);
    }
}
