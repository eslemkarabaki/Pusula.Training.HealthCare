using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.RadioloyRequestItems;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.RadioloyRequestItems;

[RemoteService]
[Area("app")]
[ControllerName("RadioloyRequestItem")]
[Route("api/app/radioloy-request-items")]
public class RadioloyRequestItemController : HealthCareController, IRadiologyRequestItemsAppService
{
    protected IRadiologyRequestItemsAppService _radiologyRequestItemsAppService;

    public RadioloyRequestItemController(IRadiologyRequestItemsAppService radiologyRequestItemsAppService)
    {
        _radiologyRequestItemsAppService = radiologyRequestItemsAppService;
    }

    [HttpGet]
    [Route("{id}")]
    public virtual async Task<RadiologyRequestItemDto> GetAsync(Guid id) => await _radiologyRequestItemsAppService.GetAsync(id);

    [HttpGet("all")]
    public virtual async Task<PagedResultDto<RadiologyRequestItemDto>> GetListAsync(GetRadiologyRequestItemsInput input) => await _radiologyRequestItemsAppService.GetListAsync(input);

    [HttpGet]
    [Route("with-navigation-properties/{id}")]
    public virtual async Task<RadiologyRequestItemWithNavigationPropertiesDto> GetNavigationPropertiesAsync(Guid id) => await _radiologyRequestItemsAppService.GetNavigationPropertiesAsync(id);

    [HttpPost]
    public virtual async Task<RadiologyRequestItemDto> CreateAsync(RadiologyRequestItemCreateDto input) => await _radiologyRequestItemsAppService.CreateAsync(input);

    [HttpPut]
    [Route("{id}")]
    public virtual async Task<RadiologyRequestItemDto> UpdateAsync(Guid id, RadiologyRequestItemUpdateDto input) => await _radiologyRequestItemsAppService.UpdateAsync(id, input);

    [HttpDelete]
    [Route("{id}")]
    public virtual async Task DeleteAsync(Guid id) => await _radiologyRequestItemsAppService.DeleteAsync(id);

    [HttpGet]
    [Route("radiology-examination-lookup")]
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetRadiologyExaminationLookupAsync(LookupRequestDto input)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("radiology-request-lookup")]
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetRadiologyRequestLookupAsync(LookupRequestDto input)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyRequestItemExcelDownloadDto input) => await _radiologyRequestItemsAppService.GetListAsExcelFileAsync(input);

    [HttpGet]
    [Route("download-token")]
    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync() => await _radiologyRequestItemsAppService.GetDownloadTokenAsync();


}
