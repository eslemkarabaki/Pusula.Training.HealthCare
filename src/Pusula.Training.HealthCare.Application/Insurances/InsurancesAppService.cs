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

namespace Pusula.Training.HealthCare.Insurances
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Insurances.Default)]
    public class InsurancesAppService(
        IInsuranceRepository insuranceRepository,
        InsuranceManager insuranceManager,
        IDistributedCache<InsuranceDownloadTokenCacheItem, string> downloadTokenCache)
        : HealthCareAppService, IInsurancesAppService
    {
        #region GetList
        public virtual async Task<PagedResultDto<InsuranceDto>> GetListAsync(GetInsurancesInput input)
        {
            var totalCount = await insuranceRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await insuranceRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<InsuranceDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Insurance>, List<InsuranceDto>>(items)
            };
        }
        #endregion

        #region Get 
        public virtual async Task<InsuranceDto> GetAsync(Guid id) => ObjectMapper.Map<Insurance, InsuranceDto>(await insuranceRepository.GetAsync(id));

        #endregion

        public virtual async Task<List<InsuranceDto>> GetListInsurancesAsync() => ObjectMapper.Map<List<Insurance>, List<InsuranceDto>>(await insuranceRepository.GetListAsync());

        #region Delete
        [Authorize(HealthCarePermissions.Insurances.Delete)]


        public virtual async Task DeleteAsync(Guid id) => await insuranceRepository.DeleteAsync(id,autoSave:true);

        #endregion

        #region Create
        [Authorize(HealthCarePermissions.Insurances.Create)]
        public virtual async Task<InsuranceDto> CreateAsync(InsuranceCreateDto input)
        {
            var insurance = await insuranceManager.CreateAsync(input.Name);

            return ObjectMapper.Map<Insurance, InsuranceDto>(insurance);
        }
        #endregion

        #region Update
        [Authorize(HealthCarePermissions.Insurances.Edit)]
        public virtual async Task<InsuranceDto> UpdateAsync(Guid id, InsuranceUpdateDto input)
        {
            var insurance = await insuranceRepository.GetAsync(id);
            await insuranceManager.UpdateAsync(id, input.Name);

            return ObjectMapper.Map<Insurance, InsuranceDto>(insurance);
        }
        #endregion

        #region GetListAsExcelFile
        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(InsuranceExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await insuranceRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Insurance>, List<InsuranceExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Insurances.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        #endregion

        #region DeleteById
        [Authorize(HealthCarePermissions.Insurances.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> insuranceIds) => await insuranceRepository.DeleteManyAsync(insuranceIds);
        #endregion

        #region DeleteAll
        [Authorize(HealthCarePermissions.Insurances.Delete)]
        public virtual async Task DeleteAllAsync(GetInsurancesInput input) => await insuranceRepository.DeleteAllAsync(input.FilterText, input.Name);
        #endregion

        #region GetDownloadToken
        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new InsuranceDownloadTokenCacheItem { Token = token },
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
