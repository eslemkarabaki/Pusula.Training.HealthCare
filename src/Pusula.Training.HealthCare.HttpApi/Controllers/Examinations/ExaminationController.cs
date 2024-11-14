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
    [HttpGet("all")]
    public virtual Task<PagedResultDto<ExaminationDto>> GetListAsync(GetExaminationsInput input)
    {
        return examinationsAppService.GetListAsync(input);
    }
    [HttpGet("{id}")]
    public virtual Task<ExaminationDto> GetAsync(Guid id)
    {
        return examinationsAppService.GetAsync(id);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return examinationsAppService.DeleteAsync(id);
    }

    [HttpPost]
    public virtual Task<ExaminationDto> CreateAsync(ExaminationCreateDto input)
    {
        return examinationsAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<ExaminationDto> UpdateAsync(Guid id,ExaminationUpdateDto input)
    {
        return examinationsAppService.UpdateAsync(id, input);
    }

    [HttpGet]
    [Route("as-excel-file")]
    public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(ExaminationExcelDownloadDto input)
    {
        return examinationsAppService.GetListAsExcelFileAsync(input);
    }

    [HttpGet]
    [Route("download-token")]
    public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        return examinationsAppService.GetDownloadTokenAsync();
    }

    [HttpDelete]
    [Route("")]
    public virtual Task DeleteByIdsAsync(List<Guid> examinationsIds)
    {
        return examinationsAppService.DeleteByIdsAsync(examinationsIds);
    }

    [HttpDelete]
    [Route("all")]
    public virtual Task DeleteAllAsync(GetExaminationsInput input)
    {
        return examinationsAppService.DeleteAllAsync(input);
    }
}