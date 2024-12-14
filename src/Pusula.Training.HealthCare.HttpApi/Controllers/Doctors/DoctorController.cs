using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Doctors;

[RemoteService]
[Area("app")]
[ControllerName("Doctor")]
[Route("api/app/doctors")]
public class DoctorController : HealthCareController
{
    private readonly IDoctorAppService _doctorAppService;

    public DoctorController(IDoctorAppService doctorAppService) =>
        _doctorAppService = doctorAppService ?? throw new ArgumentNullException(nameof(doctorAppService));

    [HttpGet]
    [Route("list-doctor")]
    public virtual Task<List<DoctorDto>> GetListDoctorsAsync() => _doctorAppService.GetListDoctorsAsync();

    [HttpGet]
    [Route("list-doctor/{id}")]
    public virtual Task<List<DoctorDto>> GetListDoctorsAsync(Guid id) => _doctorAppService.GetListDoctorsAsync(id);

    // Get list of doctors with pagination
    [HttpGet]
    public virtual Task<PagedResultDto<DoctorDto>> GetListAsync(GetDoctorsInput input) =>
        _doctorAppService.GetListAsync(input);

    [HttpGet("all/with-navigation-properties")]
    public async Task<PagedResultDto<DoctorWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(
        GetDoctorsInput input
    ) =>
        await _doctorAppService.GetListWithNavigationPropertiesAsync(input);

    // Get a single doctor by ID
    [HttpGet("{id}")]
    public virtual Task<DoctorDto> GetAsync(Guid id) => _doctorAppService.GetAsync(id);

    // Create a new doctor
    [HttpPost]
    public virtual Task<DoctorDto> CreateAsync(DoctorCreateDto input) => _doctorAppService.CreateAsync(input);

    // Update an existing doctor by ID
    [HttpPut("{id}")]
    public virtual Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input) =>
        _doctorAppService.UpdateAsync(id, input);

    // Delete a doctor by ID
    [HttpDelete("{id}")]
    public virtual Task DeleteAsync(Guid id) => _doctorAppService.DeleteAsync(id);

    // Export doctor list as Excel file
    [HttpGet("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input) =>
        _doctorAppService.GetListAsExcelFileAsync(input);

    // Get a token for downloading
    [HttpGet("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => _doctorAppService.GetDownloadTokenAsync();

    // Delete multiple doctors by IDs
    [HttpDelete("")]
    public virtual Task DeleteByIdsAsync([FromBody] List<Guid> doctorIds) =>
        _doctorAppService.DeleteByIdsAsync(doctorIds);

    // Delete all doctors with specific filter
    [HttpDelete("all")]
    public virtual Task DeleteAllAsync(GetDoctorsInput input) => _doctorAppService.DeleteAllAsync(input);
}