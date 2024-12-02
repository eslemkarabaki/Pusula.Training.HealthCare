using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Doctors
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Doctors.Default)]
    public class DoctorAppService : HealthCareAppService, IDoctorAppService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly DoctorManager _doctorManager;
        private readonly IDistributedCache<DoctorDownloadTokenCacheItem, string> _downloadTokenCache;
        private readonly ILogger<DoctorAppService> _logger;

        public DoctorAppService(
            IDoctorRepository doctorRepository,
            DoctorManager doctorManager,
            IDistributedCache<DoctorDownloadTokenCacheItem, string> downloadTokenCache,
            ILogger<DoctorAppService> logger) // Logger dependency injection
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _doctorManager = doctorManager ?? throw new ArgumentNullException(nameof(doctorManager));
            _downloadTokenCache = downloadTokenCache ?? throw new ArgumentNullException(nameof(downloadTokenCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Assign logger
        }
        //For Appointment
        public async Task<List<DoctorDto>> GetListDoctorsAsync()
        {
            return ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(await _doctorRepository.GetListAsync());
        }
        // For Appointment
        public async Task<List<DoctorDto>> GetListDoctorsAsync(Guid id)
        {
            return ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(await _doctorRepository.GetListAsync(departmentId: id));
        }

        public virtual async Task<PagedResultDto<DoctorDto>> GetListAsync(GetDoctorsInput input)
        {
            var totalCount = await _doctorRepository.GetCountAsync(input.FilterText, input.FirstName, input.LastName,input.FullName,input.WorkingHours,input.TitleId, input.DepartmentId,input.HospitalId);
            var items = await _doctorRepository.GetListAsync(input.FilterText, input.FirstName, input.LastName,input.FullName,input.WorkingHours,input.TitleId, input.DepartmentId,input.HospitalId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DoctorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(items)
            };
        }

        public virtual async Task<DoctorDto> GetAsync(Guid id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
        }

        [Authorize(HealthCarePermissions.Doctors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _doctorRepository.DeleteAsync(id);
        }
        
        public async Task<DoctorDto> GetDoctorAsync(Guid id)
        {
            var doctor = await _doctorRepository.GetAsync(id);
            return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
        }
        
        [Authorize(HealthCarePermissions.Doctors.Create)]
        public virtual async Task<DoctorDto> CreateAsync(DoctorCreateDto input)
        {
            var doctor = await _doctorManager.CreateAsync(
                input.FirstName,
                input.LastName,
                input.WorkingHours,
                input.TitleId,
                input.DepartmentId,
                input.HospitalId
            );

            return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
        }

        [Authorize(HealthCarePermissions.Doctors.Edit)]
        public virtual async Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input)
        {
           var doctor= await _doctorManager.UpdateAsync(id, input.FirstName, input.LastName, input.WorkingHours, input.DepartmentId, input.TitleId, input.HospitalId, input.ConcurrencyStamp);
            return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
        }

        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input)
        {
            var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _doctorRepository.GetListAsync(input.FilterText, input.FirstName, input.LastName,input.FullName,input.WorkingHours,input.TitleId, input.DepartmentId,input.HospitalId);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Doctor>, List<DoctorExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Doctors.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HealthCarePermissions.Doctors.Delete)]
        public async Task DeleteByIdsAsync(List<Guid> doctorIds)
        {
            try
            {
                await _doctorRepository.DeleteManyAsync(doctorIds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting doctors");
                throw new UserFriendlyException("Doktorları silerken bir hata oluştu.");
            }
        }

        [Authorize(HealthCarePermissions.Doctors.Delete)]
        public virtual async Task DeleteAllAsync(GetDoctorsInput input)
        {
            await _doctorRepository.DeleteAllAsync(input.FilterText, input.FirstName, input.LastName,input.FullName, input.WorkingHours, input.TitleId, input.DepartmentId);
        }


        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _downloadTokenCache.SetAsync(
                token,
                new DoctorDownloadTokenCacheItem { Token = token },
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
