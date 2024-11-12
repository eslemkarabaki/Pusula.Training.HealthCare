using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.Appointments
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Appointments.Default)]
    public class AppointmentsAppService(
        IAppointmentRepository appointmentRepository,
        AppointmentManager appointmentManager,
        IDistributedCache<AppointmentDownloadTokenCacheItem, string> downloadTokenCache,
        IRepository<Hospital, Guid> hospitalRepository,
        IRepository<Department, Guid> departmentRepository,
        IRepository<Doctor, Guid> doctorRepository,
        IRepository<Patient, Guid> patientRepository) :HealthCareAppService, IAppointmentsAppService
    {
        public virtual async Task<PagedResultDto<AppointmentWithNavigationPropertiesDto>> GetListAsync(GetAppointmentsInput input)
        {
            var totalCount = await appointmentRepository.GetCountAsync(input.FilterText, input.AppointmentDate, input.Status, input.Notes, input.HospitalId, input.DepartmentId, input.DoctorId, input.PatientId);
            var items = await appointmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AppointmentDate, input.Status, input.Notes, input.HospitalId, input.DepartmentId, input.DoctorId, input.PatientId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AppointmentWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AppointmentWithNavigationProperties>, List<AppointmentWithNavigationPropertiesDto>>(items)
            };
        }

        //public virtual async Task<PagedResultDto<AppointmentDto>> GetListAsync(GetAppointmentsInput input)
        //{
        //    var totalCount = await appointmentRepository.GetCountAsync(input.FilterText, input.AppointmentDate, input.Status, input.Notes, input.HospitalId, input.DepartmentId, input.DoctorId, input.PatientId);
        //    var items = await appointmentRepository.GetListAsync(input.FilterText, input.AppointmentDate, input.Status, input.Notes, input.HospitalId, input.DepartmentId, input.DoctorId, input.PatientId, input.Sorting, input.MaxResultCount, input.SkipCount);

        //    return new PagedResultDto<AppointmentDto>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<Appointment>, List<AppointmentDto>>(items)
        //    };
        //}

        public virtual async Task<AppointmentWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<AppointmentWithNavigationProperties, AppointmentWithNavigationPropertiesDto>
                (await appointmentRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<AppointmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Appointment, AppointmentDto>(await appointmentRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetHospitalLookupAsync(LookupRequestDto input)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPatientLookupAsync(LookupRequestDto input)
        {
            throw new NotImplementedException();
        }

        [Authorize(HealthCarePermissions.Appointments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await appointmentRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.Appointments.Create)]
        public virtual async Task<AppointmentDto> CreateAsync(AppointmentCreateDto input)
        {
            if (input.HospitalId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Hospital"]]);
            }
            if (input.DepartmentId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Department"]]);
            }
            if (input.DoctorId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Doctor"]]);
            }
            if (input.PatientId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["Patient"]]);
            }            

            var appointment = await appointmentManager.CreateAsync(
            input.HospitalId, input.DepartmentId, input.DoctorId, input.PatientId, input.AppointmentDate, input.Status, input.Notes);

            return ObjectMapper.Map<Appointment, AppointmentDto>(appointment);
        }

        [Authorize(HealthCarePermissions.Appointments.Edit)]
        public virtual async Task<AppointmentDto> UpdateAsync(Guid id, AppointmentUpdateDto input)
        {         

            var appointment = await appointmentManager.UpdateAsync(
            id,
            input.HospitalId, input.DepartmentId, input.DoctorId, input.PatientId, input.AppointmentDate, input.Status, input.Notes);

            return ObjectMapper.Map<Appointment, AppointmentDto>(appointment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AppointmentExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var appointments = await appointmentRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.AppointmentDate, input.Status, input.Notes, input.HospitalId, input.DepartmentId, input.DoctorId, input.PatientId);
            var items = appointments.Select(item => new
            {
                item.Appointment.AppointmentDate,
                item.Appointment.Status,
                item.Appointment.Notes,

                Hospital=item.Hospital?.Name,
                Department = item.Department?.Name,
                Doctor=item.Doctor?.FirstName,
                Patient = item.Patient?.FirstName,                

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Appointments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HealthCarePermissions.Appointments.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> appointmentlIds)
        {
            await appointmentRepository.DeleteManyAsync(appointmentlIds);
        }

        [Authorize(HealthCarePermissions.Appointments.Delete)]
        public virtual async Task DeleteAllAsync(GetAppointmentsInput input)
        {
            await appointmentRepository.DeleteAllAsync(input.FilterText, input.AppointmentDate, input.Status, input.Notes, input.HospitalId, input.DepartmentId, input.DoctorId, input.PatientId);
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new AppointmentDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}
