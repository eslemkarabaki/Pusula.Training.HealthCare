using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Permissions; 
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.RadiologyRequestItems.Default)]
public class RadiologyRequestItemsAppService
    (
        IRadiologyRequestItemRepository radiologyRequestItemRepository,
        RadiologyRequestItemManager radiologyRequestItemManager,
        IDistributedCache<RadiologyRequestItemDownloadTokenCacheItem, string> downloadTokenCache
    ) : HealthCareAppService, IRadiologyRequestItemsAppService
{
    #region GetAsync
    public virtual async Task<RadiologyRequestItemDto> GetAsync(Guid id) => ObjectMapper.Map<RadiologyRequestItem, RadiologyRequestItemDto>(await radiologyRequestItemRepository.GetAsync(id));
    #endregion

    #region GetListAsync
    public virtual async Task<PagedResultDto<RadiologyRequestItemDto>> GetListAsync(GetRadiologyRequestItemsInput input)
    {
        var totalCount = await radiologyRequestItemRepository.GetCountAsync(input.FilterText, input.RequestId, input.ExaminationId, input.Result, input.ResultDate, input.State);
        var items = await radiologyRequestItemRepository.GetListAsync(input.FilterText, input.RequestId, input.ExaminationId, input.Result, input.ResultDate, input.State, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<RadiologyRequestItemDto>(totalCount, ObjectMapper.Map<List<RadiologyRequestItem>, List<RadiologyRequestItemDto>>(items));
    }
    #endregion

    #region GetNavigationPropertiesAsync
    public virtual async Task<RadiologyRequestItemWithNavigationPropertiesDto> GetNavigationPropertiesAsync(Guid id) => ObjectMapper.Map<RadiologyRequestItemWithNavigationProperties, RadiologyRequestItemWithNavigationPropertiesDto>(await radiologyRequestItemRepository.GetWithNavigationPropertiesAsync(id));
    #endregion

    #region CreateAsync
    [Authorize(HealthCarePermissions.RadiologyRequestItems.Create)]
    public virtual async Task<RadiologyRequestItemDto> CreateAsync(RadiologyRequestItemCreateDto input)
    {
        var radiologyRequestItem = await radiologyRequestItemManager.CreateAsync(input.RequestId, input.ExaminationId, input.ResultDate, input.State, input.Result);
        return ObjectMapper.Map<RadiologyRequestItem, RadiologyRequestItemDto>(radiologyRequestItem);
    }
    #endregion

    #region UpdateAsync
    [Authorize(HealthCarePermissions.RadiologyRequestItems.Edit)]
    public virtual async Task<RadiologyRequestItemDto> UpdateAsync(Guid id, RadiologyRequestItemUpdateDto input)
    {
        var radiologyRequestItem = await radiologyRequestItemManager.UpdateAsync(id, input.RequestId, input.ExaminationId, input.ResultDate, input.State, input.Result, input.ConcurrencyStamp);

        return ObjectMapper.Map<RadiologyRequestItem, RadiologyRequestItemDto>(radiologyRequestItem);
    }
    #endregion

    #region DeleteAsync
    public virtual async Task DeleteAsync(Guid id) => await radiologyRequestItemRepository.DeleteAsync(id);
    #endregion

    #region GetNavigationPropertiesAsync
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetRadiologyExaminationLookupAsync(LookupRequestDto input)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region GetRadiologyRequestLookupAsync
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetRadiologyRequestLookupAsync(LookupRequestDto input)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region GetListAsExcelFileAsync
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyRequestItemExcelDownloadDto input)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region GetDownloadToken
    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new RadiologyRequestItemDownloadTokenCacheItem { Token = token },
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