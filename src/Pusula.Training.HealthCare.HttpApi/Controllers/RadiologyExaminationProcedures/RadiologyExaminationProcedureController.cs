using Pusula.Training.HealthCare.RadiologyExaminationProcedures;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.RadiologyExaminationProcedures
{
    [RemoteService]
    [Area("app")]
    [ControllerName("RadiologyExaminationProcedure")]
    [Route("api/app/radiology-examination-procedures")]
    public class RadiologyExaminationProcedureController : HealthCareController, IRadiologyExaminationProcedureAppService
    {
        protected IRadiologyExaminationProcedureAppService _radiologyExaminationProcedureAppService;

        public RadiologyExaminationProcedureController(IRadiologyExaminationProcedureAppService radiologyExaminationProcedureAppService)
        {
            _radiologyExaminationProcedureAppService = radiologyExaminationProcedureAppService;
        }

        [HttpPost]
        public virtual Task<RadiologyExaminationProcedureDto> CreateAsync(RadiologyExaminationProcedureCreateDto input)
        {
            return _radiologyExaminationProcedureAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetRadiologyExaminationProceduresInput input)
        {
            return _radiologyExaminationProcedureAppService.DeleteAllAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _radiologyExaminationProcedureAppService.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> RadiologyExaminationProcedureIds)
        {
            return _radiologyExaminationProcedureAppService.DeleteByIdsAsync(RadiologyExaminationProcedureIds);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RadiologyExaminationProcedureDto> GetAsync(Guid id)
        {
            return _radiologyExaminationProcedureAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<RadiologyExaminationProcedureDto>> GetListAsync(GetRadiologyExaminationProceduresInput input)
        {
            return _radiologyExaminationProcedureAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RadiologyExaminationProcedureDto> UpdateAsync(Guid id, RadiologyExaminationProcedureUpdateDto input)
        {
            return _radiologyExaminationProcedureAppService.UpdateAsync(id, input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _radiologyExaminationProcedureAppService.GetDownloadTokenAsync();
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationProcedureExcelDownloadDto input)
        {
            return _radiologyExaminationProcedureAppService.GetListAsExcelFileAsync(input);
        }
    }
}
