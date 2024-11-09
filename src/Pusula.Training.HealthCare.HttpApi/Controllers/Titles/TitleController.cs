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
    public class TitleController : HealthCareController, ITitlesAppService
    {
        protected ITitlesAppService _titlesAppService;

        public TitleController(ITitlesAppService titlesAppService)
        {
            _titlesAppService = titlesAppService;
        }

        [HttpGet]
        [Route("list")]
        public virtual Task<PagedResultDto<TitleDto>> GetListAsync(GetTitlesInput input)
        {
            return _titlesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("get/{id}")]
        public virtual Task<TitleDto> GetAsync(Guid id)
        {
            return _titlesAppService.GetAsync(id);
        }

        [HttpPost]
        [Route("create")]
        public virtual Task<TitleDto> CreateAsync(TitleCreateDto input)
        {
            return _titlesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("update/{id}")]
        public virtual Task<TitleDto> UpdateAsync(Guid id, TitleUpdateDto input)
        {
            return _titlesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _titlesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("export/excel")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(TitleExcelDownloadDto input)
        {
            return _titlesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download/token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _titlesAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("delete/multiple")]
        public virtual Task DeleteByIdsAsync(List<Guid> titleIds)
        {
            return _titlesAppService.DeleteByIdsAsync(titleIds);
        }

        [HttpDelete]
        [Route("delete/all")]
        public virtual Task DeleteAllAsync(GetTitlesInput input)
        {
            return _titlesAppService.DeleteAllAsync(input);
        }
    }
}
