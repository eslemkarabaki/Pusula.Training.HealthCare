using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Examinations;
[RemoteService]
[Area("app")]
[ControllerName("Examination")]
[Route("api/app/examinations")]
public class ExaminationController(IExaminationAppService examinationsAppService) : HealthCareController, IExaminationAppService
{
    [HttpPost]
    public async Task<ExaminationDto> CreateAsync(ExaminationCreateDto input) => await  examinationsAppService.CreateAsync(input);
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id) => await examinationsAppService.DeleteAsync(id);
    [HttpGet("{id}")]
    public async Task<ExaminationDto> GetAsync(Guid id) => await examinationsAppService.GetAsync(id);
    [HttpGet("get-list-with-navigation-properties")]
    [ActionName("GetListWithNavigationProperties")]
    public async Task<PagedResultDto<ExaminationWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(GetExaminationsInput input) => await examinationsAppService.GetListWithNavigationPropertiesAsync(input);

    [HttpGet("get-by-protocolNo/{protocolNo}")]
    [ActionName("GetWithNavigationProperties")]
    public async Task<ExaminationWithNavigationPropertiesDto> GetWithProtocolNoAsync(GetExaminationsInput input) => await examinationsAppService.GetWithProtocolNoAsync(input);

    [HttpPut("{id}")]
    public async Task<ExaminationDto> UpdateAsync(Guid id, ExaminationUpdateDto input) => await examinationsAppService.UpdateAsync(id: id,input:input);
}