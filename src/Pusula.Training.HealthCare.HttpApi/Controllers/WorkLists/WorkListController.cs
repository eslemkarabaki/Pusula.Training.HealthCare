using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.WorkLists;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.WorkLists
{
    [RemoteService]
    [Area("app")]
    [ControllerName("WorkList")]
    [Route("api/app/WorkLists")]
    public class WorkListController(IWorkListsAppService WorkListsAppService) : HealthCareController, IWorkListsAppService
    {
        [HttpPost]
        public virtual Task<WorkListDto> CreateAsync(WorkListCreateDto input)
        {
            return WorkListsAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return WorkListsAppService.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> WorkListIds)
        {
            return WorkListsAppService.DeleteByIdsAsync(WorkListIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetWorkListsInput input)
        {
            return WorkListsAppService.DeleteAllAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<WorkListDto> GetAsync(Guid id)
        {
            return WorkListsAppService.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<WorkListDto>> GetListAsync(GetWorkListsInput input)
        {
            return WorkListsAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<WorkListDto> UpdateAsync(Guid id, WorkListUpdateDto input)
        {
            return WorkListsAppService.UpdateAsync(id, input);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(WorkListExcelDownloadDto input)
        {
            return WorkListsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return WorkListsAppService.GetDownloadTokenAsync();
        }
    }
}
