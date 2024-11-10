using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Titles
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Title")]
    [Route("api/app/titles")]
    public class TitleController : HealthCareController, ITitleAppService
    {
        protected ITitleAppService TitleAppService;

        public TitleController(ITitleAppService titleAppService)
        {
            TitleAppService = titleAppService;
        }

        [HttpGet]
        [Route("list")]
        public virtual Task<PagedResultDto<TitleDto>> GetListAsync(GetTitlesInput input)
        {
            return TitleAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("get/{id}")]
        public virtual Task<TitleDto> GetAsync(Guid id)
        {
            return TitleAppService.GetAsync(id);
        }

        [HttpPost]
        [Route("create")]
        public virtual Task<TitleDto> CreateAsync(TitleCreateDto input)
        {
            return TitleAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<TitleDto> UpdateAsync(Guid id, TitleUpdateDto input)
        {
            return TitleAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return TitleAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("export/excel")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TitleExcelDownloadDto input)
        {
            return TitleAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download/token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return TitleAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("delete/multiple")]
        public virtual Task DeleteByIdsAsync(List<Guid> titleIds)
        {
            return TitleAppService.DeleteByIdsAsync(titleIds);
        }

        [HttpDelete]
        [Route("delete/all")]
        public virtual Task DeleteAllAsync(GetTitlesInput input)
        {
            return TitleAppService.DeleteAllAsync(input);
        }
    }
}
