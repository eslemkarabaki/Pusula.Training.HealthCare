using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    public interface IAppointmentReportsAppService : IApplicationService
    {
        Task<PagedResultDto<AppointmentReportDto>> GetListAsync(GetAppointmentReportsInput input);
        Task<AppointmentReportWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
        Task<AppointmentReportDto> GetAsync(Guid id);
        //Task<PagedResultDto<LookupDto<Guid>>> GetAppointmentLookupAsync(LookupRequestDto input);        
        Task DeleteAsync(Guid id);
        Task<AppointmentReportDto> CreateAsync(AppointmentReportCreateDto input);
        Task<AppointmentReportDto> UpdateAsync(Guid id, AppointmentReportUpdateDto input);
       // Task<IRemoteStreamContent> GetListAsExcelFileAsync(AppointmentReportExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> appointmentReportIds);
        Task DeleteAllAsync(GetAppointmentReportsInput input);
        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
