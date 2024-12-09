using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.RadiologyExaminationGroups;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos; 
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.RadiologyExaminationGroups
{
    [RemoteService]
    [Area("app")]
    [ControllerName("RadiologyExaminationGroup")]
    [Route("api/app/radiology-examination-groups")] 
    public class RadiologyExaminationGroupController : HealthCareController, IRadiologyExaminationGroupAppService
    {
        protected IRadiologyExaminationGroupAppService _radiologyExaminationGroupAppService;

        public RadiologyExaminationGroupController(IRadiologyExaminationGroupAppService radiologyExaminationGroupAppService)
        {
            _radiologyExaminationGroupAppService = radiologyExaminationGroupAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<RadiologyExaminationGroupDto>> GetListAsync(GetRadiologyExaminationGroupsInput input)
        { 
            return _radiologyExaminationGroupAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RadiologyExaminationGroupDto> GetAsync(Guid id)
        {
            return _radiologyExaminationGroupAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<RadiologyExaminationGroupDto> CreateAsync(RadiologyExaminationGroupCreateDto input)
        {
            return _radiologyExaminationGroupAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RadiologyExaminationGroupDto> UpdateAsync(Guid id, RadiologyExaminationGroupUpdateDto input)
        {
            return _radiologyExaminationGroupAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _radiologyExaminationGroupAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationGroupExcelDownloadDto input)
        {
            return _radiologyExaminationGroupAppService.GetListAsExcelFileAsync(input);
        }

        [HttpDelete]
        [Route("")]
        public Task DeleteByIdsAsync(List<Guid> RadiologyExaminationGroupIds)
        {
            return _radiologyExaminationGroupAppService.DeleteByIdsAsync(RadiologyExaminationGroupIds);
        }

        [HttpDelete]
        [Route("all")]
        public Task DeleteAllAsync(GetRadiologyExaminationGroupsInput input)
        {
            return _radiologyExaminationGroupAppService.DeleteAllAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _radiologyExaminationGroupAppService.GetDownloadTokenAsync();
        }
    }
}
