using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Departments;
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

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.AppointmentTypes.Default)]
    public class AppointmentTypesAppService(
        IAppointmentTypeRepository appointmentTypeRepository,
        AppointmentTypeManager appointmentTypeManager,
        IDistributedCache<AppointmentTypeDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IAppointmentTypesAppService
    {
        #region GetList
        public virtual async Task<PagedResultDto<AppointmentTypeDto>> GetListAsync(GetAppointmentTypesInput input)
        {
            var totalCount = await appointmentTypeRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await appointmentTypeRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AppointmentTypeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AppointmentType>, List<AppointmentTypeDto>>(items)
            };
        }
        #endregion

        #region Get 
        public virtual async Task<AppointmentTypeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AppointmentType, AppointmentTypeDto>(await appointmentTypeRepository.GetAsync(id));
        }
        #endregion

        #region Delete
        [Authorize(HealthCarePermissions.AppointmentTypes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await appointmentTypeRepository.DeleteAsync(id);
        }
        #endregion

        #region Create
        [Authorize(HealthCarePermissions.AppointmentTypes.Create)]
        public virtual async Task<AppointmentTypeDto> CreateAsync(AppointmentTypeCreateDto input)
        {

            var appointmentType = await appointmentTypeManager.CreateAsync(
            input.Name
            );

            return ObjectMapper.Map<AppointmentType, AppointmentTypeDto>(appointmentType);
        }
        #endregion

        #region Update
        [Authorize(HealthCarePermissions.AppointmentTypes.Edit)]
        public virtual async Task<AppointmentTypeDto> UpdateAsync(Guid id, AppointmentTypeUpdateDto input)
        {
            var appointmentType = await appointmentTypeRepository.GetAsync(id);
            await appointmentTypeManager.UpdateAsync(
            id,
            input.Name
            );

            return ObjectMapper.Map<AppointmentType, AppointmentTypeDto>(appointmentType);
        }
        #endregion

        #region GetListAsExcelFile
        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(AppointmentTypeExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await appointmentTypeRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<AppointmentType>, List<AppointmentTypeExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "AppointmentTypes.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        #endregion

        #region DeleteById
        [Authorize(HealthCarePermissions.AppointmentTypes.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> appointmentTypeIds)
        {
            await appointmentTypeRepository.DeleteManyAsync(appointmentTypeIds);
        }
        #endregion

        #region DeleteAll
        [Authorize(HealthCarePermissions.AppointmentTypes.Delete)]
        public virtual async Task DeleteAllAsync(GetAppointmentTypesInput input)
        {
            await appointmentTypeRepository.DeleteAllAsync(input.FilterText, input.Name);
        }
        #endregion

        #region GetDownloadToken
        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new AppointmentTypeDownloadTokenCacheItem { Token = token },
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
    }
}
