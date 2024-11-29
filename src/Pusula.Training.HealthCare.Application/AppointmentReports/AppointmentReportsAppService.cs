using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.AppointmentReports
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.AppointmentReports.Default)]
    public class AppointmentReportsAppService(
        IAppointmentReportRepository appointmentReportRepository,
        AppointmentReportManager appointmentReportManager,
        IDistributedCache<AppointmentReportDownloadTokenCacheItem, string> downloadTokenCache,
                IRepository<Appointment, Guid> appointmentRepository,
                IRepository<Patient,Guid> patientRepository) :HealthCareAppService, IAppointmentReportsAppService
    {
        #region GetList
        public virtual async Task<PagedResultDto<AppointmentReportDto>> GetListAsync(GetAppointmentReportsInput input)
        {
            var totalCount = await appointmentReportRepository.GetCountAsync(input.FilterText, input.ReportDate, input.PriorityNotes, input.DoctorNotes, input.AppointmentId);
            var items = await appointmentReportRepository.GetListAsync(input.FilterText, input.ReportDate, input.PriorityNotes, input.DoctorNotes, input.AppointmentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AppointmentReportDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AppointmentReport>, List<AppointmentReportDto>>(items)
            };
        }
        #endregion

        #region GetWithNavigationProperties
        public virtual async Task<AppointmentReportWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<AppointmentReportWithNavigationProperties, AppointmentReportWithNavigationPropertiesDto>
                (await appointmentReportRepository.GetWithNavigationPropertiesAsync(id));
        }
        #endregion        

        #region Get
        public virtual async Task<AppointmentReportDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AppointmentReport, AppointmentReportDto>(await appointmentReportRepository.GetAsync(id));
        }
        #endregion

        #region Delete
        [Authorize(HealthCarePermissions.AppointmentReports.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await appointmentReportRepository.DeleteAsync(id);
        }
        #endregion

        #region Create
        [Authorize(HealthCarePermissions.AppointmentReports.Create)]
        public virtual async Task<AppointmentReportDto> CreateAsync(AppointmentReportCreateDto input)
        {
            if (input.AppointmentId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Appointment"]]);
            }            

            var appointmentReport = await appointmentReportManager.CreateAsync(
            input.AppointmentId,input.ReportDate, input.PriorityNotes, input.DoctorNotes);

            return ObjectMapper.Map<AppointmentReport, AppointmentReportDto>(appointmentReport);
        }
        #endregion

        #region Update
        [Authorize(HealthCarePermissions.AppointmentReports.Edit)]
        public virtual async Task<AppointmentReportDto> UpdateAsync(Guid id, AppointmentReportUpdateDto input)
        {

            var appointmentReport = await appointmentReportManager.UpdateAsync(
                id, input.AppointmentId, input.ReportDate, input.PriorityNotes, input.DoctorNotes);

            return ObjectMapper.Map<AppointmentReport, AppointmentReportDto>(appointmentReport);
        }
        #endregion    

        #region DeleteById
        [Authorize(HealthCarePermissions.AppointmentReports.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> appointmentReportIds)
        {
            await appointmentReportRepository.DeleteManyAsync(appointmentReportIds);
        }
        #endregion

        #region DeleteAll
        [Authorize(HealthCarePermissions.AppointmentReports.Delete)]
        public virtual async Task DeleteAllAsync(GetAppointmentReportsInput input)
        {
            await appointmentReportRepository.DeleteAllAsync(input.FilterText, input.ReportDate, input.PriorityNotes, input.DoctorNotes, input.AppointmentId);
        }
        #endregion

        #region GetDownloadToken
        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new AppointmentReportDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
        #endregion

        //public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetAppointmentLookupAsync(LookupRequestDto input)
        //{
        //    // Appointment ve Patient tablolarını birleştiriyoruz
        //    var query = from appointment in (await appointmentRepository.GetQueryableAsync())
        //                join patient in (await patientRepository.GetQueryableAsync())
        //                    on appointment.PatientId equals patient.Id
        //                where string.IsNullOrWhiteSpace(input.Filter) || patient.FirstName.Contains(input.Filter)
        //                select new
        //                {
        //                    appointment.Id,
        //                    appointment.AppointmentStartDate,
        //                    appointment.PatientId,
        //                    PatientName = patient.FirstName
        //                };

        //    var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Appointment>();
        //    var totalCount = query.Count();
        //    return new PagedResultDto<LookupDto<Guid>>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<Appointment>, List<LookupDto<Guid>>>(lookupData)
        //    };
        //}
    }
}
