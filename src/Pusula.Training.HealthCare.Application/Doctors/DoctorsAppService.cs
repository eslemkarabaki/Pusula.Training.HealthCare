using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
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
using Volo.Abp.ObjectMapping;
using Pusula.Training.HealthCare.DoctorWithDepartments;

namespace Pusula.Training.HealthCare.Doctors
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Doctors.Default)]
    public class DoctorsAppService : HealthCareAppService, IDoctorsAppService
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly DoctorManager doctorManager;
        private readonly IDistributedCache<DoctorDownloadTokenCacheItem, string> downloadTokenCache;

        public DoctorsAppService(
            IDoctorRepository doctorRepository,
            DoctorManager doctorManager,
            IDistributedCache<DoctorDownloadTokenCacheItem, string> downloadTokenCache)
        {
            this.doctorRepository = doctorRepository;
            this.doctorManager = doctorManager;
            this.downloadTokenCache = downloadTokenCache;
        }

        public virtual async Task<PagedResultDto<DoctorDto>> GetListAsync(GetDoctorsInput input)
        {
            var totalCount = await doctorRepository.GetCountAsync(input.FilterText, input.FirstName, input.LastName, input.DepartmentId);
            var items = await doctorRepository.GetListAsync(input.FilterText, input.FirstName, input.LastName, input.DepartmentId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DoctorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(items)
            };
        }

        public virtual async Task<DoctorDto> GetAsync(Guid id)
        {
            var doctor = await doctorRepository.GetAsync(id);
            return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
        }

        [Authorize(HealthCarePermissions.Doctors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await doctorRepository.DeleteAsync(id);
        }

        [Authorize(HealthCarePermissions.Doctors.Create)]
        public virtual async Task<DoctorDto> CreateAsync(DoctorCreateDto input)
        {
            var doctor = await doctorManager.CreateAsync(
                input.FirstName,
                input.LastName,
                input.WorkingHours,
                input.DepartmentId,
                input.TitleId,
                input.HospitalId
            );

            return ObjectMapper.Map<Doctor, DoctorDto>(doctor);
        }

        [Authorize(HealthCarePermissions.Doctors.Edit)]
        public virtual async Task<DoctorDto> UpdateAsync(Guid id, DoctorUpdateDto input)
        {
            await doctorManager.UpdateAsync(id, input.FirstName, input.LastName, input.WorkingHours, input.DepartmentId, input.TitleId, input.HospitalId, input.ConcurrencyStamp);

            var updatedDoctor = await doctorRepository.GetAsync(id);
            return ObjectMapper.Map<Doctor, DoctorDto>(updatedDoctor);
        }

        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await doctorRepository.GetListAsync(input.FilterText, input.FirstName, input.LastName, input.TitleId, input.DepartmentId);

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
                await doctorRepository.DeleteManyAsync(doctorIds);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Doktorları silerken bir hata oluştu.");
            }
        }

        [Authorize(HealthCarePermissions.Doctors.Delete)]
        public virtual async Task DeleteAllAsync(GetDoctorsInput input)
        {
            await doctorRepository.DeleteAllAsync(input.FilterText, input.FirstName, input.LastName, input.WorkingHours, input.TitleId, input.DepartmentId);
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
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
