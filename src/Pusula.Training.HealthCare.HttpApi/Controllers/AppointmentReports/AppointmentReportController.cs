using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
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
        public virtual Task<PagedResultDto<AppointmentReportDto>> GetListAsync(GetAppointmentReportsInput input)
        {
            return _appointmentReportsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<AppointmentReportWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _appointmentReportsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AppointmentReportDto> GetAsync(Guid id)
        {
            return _appointmentReportsAppService.GetAsync(id);
        }

        //[HttpGet]
        //[Route("appointment-lookup")]
        //public virtual Task<PagedResultDto<LookupDto<Guid>>> GetAppointmentLookupAsync(LookupRequestDto input)
        //{
        //    return _appointmentReportsAppService.GetAppointmentLookupAsync(input);
        //}      
                       

        [HttpPost]
        public virtual Task<AppointmentReportDto> CreateAsync(AppointmentReportCreateDto input)
        {
            return _appointmentReportsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AppointmentReportDto> UpdateAsync(Guid id, AppointmentReportUpdateDto input)
        {
            return _appointmentReportsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _appointmentReportsAppService.DeleteAsync(id);
        }

        //[HttpGet]
        //[Route("as-excel-file")]
        //public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(AppointmentReportExcelDownloadDto input)
        //{
        //    return _appointmentReportsAppService.GetListAsExcelFileAsync(input);
        //}

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _appointmentReportsAppService.GetDownloadTokenAsync();
        }

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> appointmentReportIds)
        {
            return _appointmentReportsAppService.DeleteByIdsAsync(appointmentReportIds);
        }

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetAppointmentReportsInput input)
        {
            return _appointmentReportsAppService.DeleteAllAsync(input);
        }
    }
}
