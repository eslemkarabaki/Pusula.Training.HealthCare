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

namespace Pusula.Training.HealthCare.Hospitals
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Hospitals.Default)]
    public class HospitalsAppService(IHospitalRepository hospitalRepository,
        HospitalManager hospitalManager, IDistributedCache<HospitalDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IHospitalsAppService
    {
        public virtual async Task<PagedResultDto<HospitalDto>> GetListAsync(GetHospitalsInput input)
        {
            var totalCount = await hospitalRepository.GetCountAsync(input.FilterText, input.Name, input.Address);
            var items = await hospitalRepository.GetListAsync(input.FilterText, input.Name, input.Address, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<HospitalDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Hospital>, List<HospitalDto>>(items)
            };
        }

        public virtual async Task<HospitalDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<HospitalWithDepartment, HospitalDto>(await hospitalRepository.GetAsync(id));
        }

        [Authorize(HealthCarePermissions.Hospitals.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await hospitalManager.DeleteAsyncHospitalWithDepartment(id);
        }

        [Authorize(HealthCarePermissions.Hospitals.Create)]
        public virtual async Task<HospitalDto> CreateAsync(HospitalCreateDto input)
        {

            var hospital = await hospitalManager.CreateAsync(
            input.Name,
            input.Address,
            input.DepartmentNames
            );

            return ObjectMapper.Map<Hospital, HospitalDto>(hospital);
        }

        [Authorize(HealthCarePermissions.Hospitals.Edit)]
        public virtual async Task<HospitalDto> UpdateAsync(Guid id, HospitalUpdateDto input)
        {
            if (input.DepartmentNames == null)
            {
                input.DepartmentNames = Array.Empty<string>();
            }

            var hospital = await hospitalManager.UpdateAsync(id, input.Name, input.Address, input.DepartmentNames, input.ConcurrencyStamp);

            return ObjectMapper.Map<Hospital, HospitalDto>(hospital);
        }


        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(HospitalExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await hospitalRepository.GetListAsync(input.FilterText, input.Name, input.Address);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Hospital>, List<HospitalExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Hospitals.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HealthCarePermissions.Hospitals.Delete)]
        public async Task DeleteByIdsAsync(List<Guid> hospitalIds)
        {
            await hospitalRepository.DeleteManyAsync(hospitalIds);
        }

        [Authorize(HealthCarePermissions.Hospitals.Delete)]
        public virtual async Task DeleteAllAsync(GetHospitalsInput input)
        {
            await hospitalRepository.DeleteAllAsync(input.FilterText, input.Name, input.Address);
        }  

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new HospitalDownloadTokenCacheItem { Token = token },
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
