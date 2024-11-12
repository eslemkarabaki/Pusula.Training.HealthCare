using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Patients;

[RemoteService]
[Area("app")]
[ControllerName("Patient")]
[Route("api/app/patients")]
public class PatientController(IPatientsAppService patientsAppService) : HealthCareController, IPatientsAppService
{
    [HttpGet("all")]
    public virtual Task<PagedResultDto<PatientDto>> GetListAsync(GetPatientsInput input)
    {
        return patientsAppService.GetListAsync(input);
    }

    [HttpGet("all/with-address-and-country")]
    public Task<PagedResultDto<PatientDto>> GetListWithAddressAndCountryAsync(GetPatientsInput input)
    {
        return patientsAppService.GetListWithAddressAndCountryAsync(input);
    }

    [HttpGet("{id}")]
    public virtual Task<PatientDto> GetAsync(Guid id)
    {
        return patientsAppService.GetAsync(id);
    }

    [HttpPost]
    public virtual Task<PatientDto> CreateAsync(PatientCreateDto input)
    {
        return patientsAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<PatientDto> UpdateAsync(Guid id, PatientUpdateDto input)
    {
        return patientsAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return patientsAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientExcelDownloadDto input)
    {
        return patientsAppService.GetListAsExcelFileAsync(input);
    }

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return patientsAppService.GetDownloadTokenAsync();
    }

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> patientIds)
    {
        return patientsAppService.DeleteByIdsAsync(patientIds);
    }

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetPatientsInput input)
    {
        return patientsAppService.DeleteAllAsync(input);
    }
}