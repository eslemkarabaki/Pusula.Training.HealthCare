using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp;
using Pusula.Training.HealthCare.AppointmentTypes;

namespace Pusula.Training.HealthCare.Controllers.AppointmentTypes
{
    [RemoteService]
    [Area("app")]
    [ControllerName("AppointmentType")]
    [Route("api/app/appointmentTypes")]
    public class AppointmentTypeController : HealthCareController, IAppointmentTypesAppService
    {
        protected IAppointmentTypesAppService _appointmentTypesAppService;

        public AppointmentTypeController(IAppointmentTypesAppService appointmentTypesAppService)
        {
            _appointmentTypesAppService = appointmentTypesAppService;
        }
        
        [HttpGet]
        [Route("list-appoinmentTypes")]
        public virtual Task<List<AppointmentTypeDto>> GetListAppointmentTypesAsync() => _appointmentTypesAppService.GetListAppointmentTypesAsync();
        
        [HttpGet("all")]
        public virtual Task<PagedResultDto<AppointmentTypeDto>> GetListAsync(GetAppointmentTypesInput input) =>  _appointmentTypesAppService.GetListAsync(input);

        [HttpGet]
        public virtual Task<List<AppointmentTypeDto>> GetListAsync() => _appointmentTypesAppService.GetListAsync();

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AppointmentTypeDto> GetAsync(Guid id) =>  _appointmentTypesAppService.GetAsync(id);
             
        [HttpPost]
        public virtual Task<AppointmentTypeDto> CreateAsync(AppointmentTypeCreateDto input) => _appointmentTypesAppService.CreateAsync(input);        

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AppointmentTypeDto> UpdateAsync(Guid id, AppointmentTypeUpdateDto input) => _appointmentTypesAppService.UpdateAsync(id, input);

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => _appointmentTypesAppService.DeleteAsync(id);
        
        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(AppointmentTypeExcelDownloadDto input) => _appointmentTypesAppService.GetListAsExcelFileAsync(input);
        
        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => _appointmentTypesAppService.GetDownloadTokenAsync();
        
        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> appointmentTypeIds) => _appointmentTypesAppService.DeleteByIdsAsync(appointmentTypeIds);
        
        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetAppointmentTypesInput input) =>  _appointmentTypesAppService.DeleteAllAsync(input);
        
    }
}
