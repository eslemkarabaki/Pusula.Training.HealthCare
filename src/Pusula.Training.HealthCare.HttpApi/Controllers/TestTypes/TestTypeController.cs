using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.TestTypes;

[RemoteService]
[Area("app")]
[ControllerName("TestType")]
[Route("api/app/test-types")]
public class TestTypeController(ITestTypesAppService testTypesAppService) : HealthCareController, ITestTypesAppService
{
    [HttpPost]
    public virtual Task<TestTypeDto> CreateAsync(TestTypeCreateDto input)
    {
        return testTypesAppService.CreateAsync(input);
    }

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetTestTypesInput input)
    {
        return testTypesAppService.DeleteAllAsync(input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return testTypesAppService.DeleteAsync(id);
    }

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> testTypeIds)
    {
        return testTypesAppService.DeleteByIdsAsync(testTypeIds);
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<TestTypeDto> GetAsync(Guid id)
    {
        return testTypesAppService.GetAsync(id);
    }

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return testTypesAppService.GetDownloadTokenAsync();
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestTypeExcelDownloadDto input)
    {
        return testTypesAppService.GetListAsExcelFileAsync(input);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<TestTypeDto>> GetListAsync(GetTestTypesInput input)
    {
        return testTypesAppService.GetListAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<TestTypeDto> UpdateAsync(Guid id, TestTypeUpdateDto input)
    {
        return testTypesAppService.UpdateAsync(id, input);
    }
}
