using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.RadiologyExaminations
{
    [RemoteService]
    [Area("app")]
    [ControllerName("RadiologyExamination")]
    [Route("api/app/radiology-examinations")]
    public class RadiologyExaminationController : HealthCareController, IRadiologyExaminationAppService
    {
        protected IRadiologyExaminationAppService _radiologyExaminationsAppService;

        public RadiologyExaminationController(IRadiologyExaminationAppService radiologyExaminationsAppService)
        {
            _radiologyExaminationsAppService = radiologyExaminationsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<RadiologyExaminationDto>> GetListAsync(GetRadiologyExaminationsInput input)
        {
            return _radiologyExaminationsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RadiologyExaminationDto> GetAsync(Guid id)
        {
            return _radiologyExaminationsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<RadiologyExaminationDto> CreateAsync(RadiologyExaminationCreateDto input)
        {
            return _radiologyExaminationsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RadiologyExaminationDto> UpdateAsync(Guid id, RadiologyExaminationUpdateDto input)
        {
            return _radiologyExaminationsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _radiologyExaminationsAppService.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetRadiologyExaminationsInput input)
        {
            return _radiologyExaminationsAppService.DeleteAllAsync(input);
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> RadiologyExaminationIds)
        {
            return _radiologyExaminationsAppService.DeleteByIdsAsync(RadiologyExaminationIds);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _radiologyExaminationsAppService.GetDownloadTokenAsync();
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationExcelDownloadDto input)
        {
            return _radiologyExaminationsAppService.GetListAsExcelFileAsync(input);
        }
    }
}
