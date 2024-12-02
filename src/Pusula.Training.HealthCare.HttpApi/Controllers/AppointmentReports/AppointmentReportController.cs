using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using Pusula.Training.HealthCare.AppointmentReports;

namespace Pusula.Training.HealthCare.Controllers.AppointmentReports
{
    [RemoteService]
    [Area("app")]
    [ControllerName("AppointmentReport")]
    [Route("api/app/appointmentReports")]
    public class AppointmentReportController : HealthCareController, IAppointmentReportsAppService
    {
        protected IAppointmentReportsAppService _appointmentReportsAppService;

        public AppointmentReportController(IAppointmentReportsAppService appointmentReportsAppService)
        {
            _appointmentReportsAppService = appointmentReportsAppService;
        }
        
        [HttpGet("all")]
        public virtual Task<PagedResultDto<AppointmentReportDto>> GetListAsync(GetAppointmentReportsInput input)=> _appointmentReportsAppService.GetListAsync(input);

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<AppointmentReportWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id) => _appointmentReportsAppService.GetWithNavigationPropertiesAsync(id);
        
        [HttpGet]
        [Route("{id}")]
        public virtual Task<AppointmentReportDto> GetAsync(Guid id) => _appointmentReportsAppService.GetAsync(id);

        [HttpPost]
        public virtual Task<AppointmentReportDto> CreateAsync(AppointmentReportCreateDto input) => _appointmentReportsAppService.CreateAsync(input);
        
        [HttpPut]
        [Route("{id}")]
        public virtual Task<AppointmentReportDto> UpdateAsync(Guid id, AppointmentReportUpdateDto input) => _appointmentReportsAppService.UpdateAsync(id, input);
        
        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => _appointmentReportsAppService.DeleteAsync(id);        

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => _appointmentReportsAppService.GetDownloadTokenAsync();
        
        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> appointmentReportIds) => _appointmentReportsAppService.DeleteByIdsAsync(appointmentReportIds);
        
        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetAppointmentReportsInput input) =>  _appointmentReportsAppService.DeleteAllAsync(input);
        
    }
}
