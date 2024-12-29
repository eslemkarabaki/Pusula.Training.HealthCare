using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Hospitals;

[RemoteService]
[Area("app")]
[ControllerName("Hospital")]
[Route("api/app/hospitals")]
public class HospitalController(IHospitalsAppService hospitalsAppService) : HealthCareController, IHospitalsAppService
{

    [HttpPost]
    public virtual Task<HospitalDto> CreateAsync(HospitalCreateDto input)
    {
        return hospitalsAppService.CreateAsync(input);
    }

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetHospitalsInput input)
    {
        return hospitalsAppService.DeleteAllAsync(input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return hospitalsAppService.DeleteAsync(id);
    }

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> hospitalIds)
    {
        return hospitalsAppService.DeleteByIdsAsync(hospitalIds);
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<HospitalDto> GetAsync(Guid id)
    {
        return hospitalsAppService.GetAsync(id);
    }

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return hospitalsAppService.GetDownloadTokenAsync();
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(HospitalExcelDownloadDto input)
    {
        return hospitalsAppService.GetListAsExcelFileAsync(input);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<HospitalDto>> GetListAsync(GetHospitalsInput input)
    {
        return hospitalsAppService.GetListAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<HospitalDto> UpdateAsync(Guid id, HospitalUpdateDto input)
    {
        return hospitalsAppService.UpdateAsync(id, input);
    }


}
