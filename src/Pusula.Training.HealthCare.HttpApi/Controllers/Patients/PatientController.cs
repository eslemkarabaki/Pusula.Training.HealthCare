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
public class PatientController(IPatientAppService patientAppService) : HealthCareController, IPatientAppService
{
    [HttpGet("get")]
    public virtual Task<PatientDto> GetAsync(GetPatientInput input) => patientAppService.GetAsync(input);

    [HttpGet("get/with-navigation-properties")]
    public virtual Task<PatientWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(GetPatientInput input) =>
        patientAppService.GetWithNavigationPropertiesAsync(input);

    
    [HttpGet("get/all")]
    public virtual Task<PagedResultDto<PatientDto>> GetListAsync(GetPatientsInput input) =>
        patientAppService.GetListAsync(input);

    [HttpGet("get/all/with-navigation-properties")]
    public Task<PagedResultDto<PatientWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(
        GetPatientsInput input
    ) =>
        patientAppService.GetListWithNavigationPropertiesAsync(input);


    [HttpGet("get/addresses-with-details/{patientId:guid}")]
    public Task<List<AddressDto>> GetPatientAddressesWithDetailsAsync(Guid patientId) =>
        patientAppService.GetPatientAddressesWithDetailsAsync(patientId);

    
    [HttpPost]
    public virtual Task<PatientDto> CreateAsync(PatientCreateDto input) => patientAppService.CreateAsync(input);

    [HttpPut]
    [Route("{id}")]
    public virtual Task<PatientDto> UpdateAsync(Guid id, PatientUpdateDto input) =>
        patientAppService.UpdateAsync(id, input);

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id) => patientAppService.DeleteAsync(id);

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientExcelDownloadDto input) =>
        patientAppService.GetListAsExcelFileAsync(input);

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => patientAppService.GetDownloadTokenAsync();

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> patientIds) => patientAppService.DeleteByIdsAsync(patientIds);

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetPatientsInput input) => patientAppService.DeleteAllAsync(input);
}