using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.TestGroups;

[RemoteService]
[Area("app")]
[ControllerName("TestGroup")]
[Route("api/app/test-groups")]
public class TestGroupController : HealthCareController, ITestGroupsAppService
{
    private readonly ITestGroupsAppService _testGroupsAppService;

    public TestGroupController(ITestGroupsAppService testGroupsAppService)
    {
        _testGroupsAppService = testGroupsAppService;
    }

    [HttpGet]
    public virtual Task<PagedResultDto<TestGroupDto>> GetListAsync(GetTestGroupsInput input)
    {
        return _testGroupsAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<TestGroupDto> GetAsync(Guid id)
    {
        return _testGroupsAppService.GetAsync(id);
    }

    [HttpPost]
    public virtual Task<TestGroupDto> CreateAsync(TestGroupCreateDto input)
    {
        return _testGroupsAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<TestGroupDto> UpdateAsync(Guid id, TestGroupUpdateDto input)
    {
        return _testGroupsAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return _testGroupsAppService.DeleteAsync(id);
    }

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> testGroupIds)
    {
        return _testGroupsAppService.DeleteByIdsAsync(testGroupIds);
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestGroupExcelDownloadDto input)
    {
        return _testGroupsAppService.GetListAsExcelFileAsync(input);
    }

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return _testGroupsAppService.GetDownloadTokenAsync();
    }
}
