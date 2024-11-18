using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Patients;

[RemoteService]
[Area("app")]
[ControllerName("Patient")]
[Route("api/app/patients")]
public class IPatientController(IPatientAppService patientAppService) : HealthCareController, IPatientAppService
{
    [HttpGet("all")]
    public virtual Task<PagedResultDto<PatientDto>> GetListAsync(GetPatientsInput input)
    {
        return patientAppService.GetListAsync(input);
    }

    [HttpGet("view/all")]
    public Task<PagedResultDto<PatientViewDto>> GetViewListAsync(GetPatientsInput input)
    {
        return patientAppService.GetViewListAsync(input);
    }

    [HttpGet("address/{id}")]
    public Task<List<AddressDto>> GetAddressListAsync(Guid patientId)
    {
        return patientAppService.GetAddressListAsync(patientId);
    }

    [HttpGet("{id}")]
    public virtual Task<PatientDto> GetAsync(Guid id)
    {
        return patientAppService.GetAsync(id);
    }

    [HttpGet("view/{id}")]
    public virtual Task<PatientViewDto> GetViewAsync(Guid id)
    {
        return patientAppService.GetViewAsync(id);
    }

    [HttpPost]
    public virtual Task<PatientDto> CreateAsync(PatientCreateDto input)
    {
        return patientAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<PatientDto> UpdateAsync(Guid id, PatientUpdateDto input)
    {
        return patientAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return patientAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientExcelDownloadDto input)
    {
        return patientAppService.GetListAsExcelFileAsync(input);
    }

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return patientAppService.GetDownloadTokenAsync();
    }

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> patientIds)
    {
        return patientAppService.DeleteByIdsAsync(patientIds);
    }

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetPatientsInput input)
    {
        return patientAppService.DeleteAllAsync(input);
    }
}