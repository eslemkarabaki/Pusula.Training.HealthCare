using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.RadiologyRequests;
using Pusula.Training.HealthCare.Shared;
using System; 
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.RadiologyRequests;

[RemoteService]
[Area("app")]
[ControllerName("RadiologyRequest")]
[Route("api/app/radiology-requests")]
public class RadiologyRequestController : HealthCareController, IRadiologyRequestsAppService
{
    protected IRadiologyRequestsAppService _radiologyRequestsAppService;

    public RadiologyRequestController(IRadiologyRequestsAppService radiologyRequestsAppService)
    {
        _radiologyRequestsAppService = radiologyRequestsAppService;
    }

    [HttpGet]
    [Route("{id}")]
    public virtual async Task<RadiologyRequestDto> GetAsync(Guid id) => await _radiologyRequestsAppService.GetAsync(id);
    
    [HttpGet("all")]
    public virtual async Task<PagedResultDto<RadiologyRequestDto>> GetListAsync(GetRadiologyRequestsInput input) => await _radiologyRequestsAppService.GetListAsync(input);
    
    [HttpGet]
    [Route("with-navigation-properties/{id}")]
    public virtual async Task<RadiologyRequestWithNavigationPropertiesDto> GetNavigationPropertiesAsync(Guid id) => await _radiologyRequestsAppService.GetNavigationPropertiesAsync(id);
    
    [HttpPost]
    public virtual async Task<RadiologyRequestDto> CreateAsync(RadiologyRequestCreateDto input) => await _radiologyRequestsAppService.CreateAsync(input);

    [HttpPut]
    [Route("{id}")]
    public virtual async Task<RadiologyRequestDto> UpdateAsync(Guid id, RadiologyRequestUpdateDto input) => await _radiologyRequestsAppService.UpdateAsync(id, input);
    
    [HttpDelete]
    [Route("{id}")]
    public virtual async Task DeleteAsync(Guid id) => await _radiologyRequestsAppService.DeleteAsync(id);

    [HttpGet]
    [Route("department-lookup")]
    public async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync([FromQuery] LookupRequestDto input)
    {
        return await _radiologyRequestsAppService.GetDepartmentLookupAsync(input);
    }

    [HttpGet]
    [Route("doctor-lookup")]
    public async Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync([FromQuery] LookupRequestDto input)
    {
        return await _radiologyRequestsAppService.GetDoctorLookupAsync(input);
    }

    [HttpGet]
    [Route("protocol-lookup")]
    public async Task<PagedResultDto<LookupDto<Guid>>> GetProtocolLookupAsync([FromQuery] LookupRequestDto input)
    {
        return await _radiologyRequestsAppService.GetProtocolLookupAsync(input);
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyRequestExcelDownloadDto input) => await _radiologyRequestsAppService.GetListAsExcelFileAsync(input);

    [HttpGet]
    [Route("download-token")]
    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync() => await _radiologyRequestsAppService.GetDownloadTokenAsync();


}