using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs; 
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadiologyRequests;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.RadiologyRequests.Default)]
public class RadiologyRequestsAppService
    (
        IRadiologyRequestRepository radiologyRequestRepository,
        RadiologyRequestManager radiologyRequestManager,
        IDistributedCache<RadiologyRequestDownloadTokenCacheItem, string> downloadTokenCache,

        IDepartmentRepository departmentRepository,
        IDoctorRepository doctorRepository,
        IProtocolRepository protocolRepository
    ) : HealthCareAppService, IRadiologyRequestsAppService
{
    #region GetAsync
    public virtual async Task<RadiologyRequestDto> GetAsync(Guid id) => ObjectMapper.Map<RadiologyRequest, RadiologyRequestDto>(await radiologyRequestRepository.GetAsync(id)); 
    #endregion

    #region GetListAsync
    public virtual async Task<PagedResultDto<RadiologyRequestDto>> GetListAsync(GetRadiologyRequestsInput input)
    {
        var totalCount = await radiologyRequestRepository.GetCountAsync(input.FilterText, input.RequestDate, input.ProtocolId, input.DepartmentId, input.DoctorId);
        var items = await radiologyRequestRepository.GetListAsync(input.FilterText, input.RequestDate, input.ProtocolId, input.DepartmentId, input.DoctorId, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<RadiologyRequestDto>(totalCount, ObjectMapper.Map<List<RadiologyRequest>, List<RadiologyRequestDto>>(items));
    }
    #endregion

    #region GetNavigationPropertiesAsync
    public virtual async Task<RadiologyRequestWithNavigationPropertiesDto> GetNavigationPropertiesAsync(Guid id) => ObjectMapper.Map<RadiologyRequestWithNavigationProperties, RadiologyRequestWithNavigationPropertiesDto>(await radiologyRequestRepository.GetWithNavigationPropertiesAsync(id));
    #endregion

    #region CreateAsync
    [Authorize(HealthCarePermissions.RadiologyRequests.Create)]
    public virtual async Task<RadiologyRequestDto> CreateAsync(RadiologyRequestCreateDto input)
    {
        var radiologyRequest = await radiologyRequestManager.CreateAsync(input.RequestDate, input.ProtocolId, input.DepartmentId, input.DoctorId);
        
        return ObjectMapper.Map<RadiologyRequest, RadiologyRequestDto>(radiologyRequest);
    }
    #endregion

    #region GetRadiologyRequestsWithNavigationPropertiesAsync
    [Authorize(HealthCarePermissions.RadiologyRequests.Default)]
    public virtual async Task<PagedResultDto<RadiologyRequestWithNavigationPropertiesDto>> GetListNavigationPropertiesAsync(GetRadiologyRequestsInput input)
    { 
        var totalCount = await radiologyRequestRepository.GetCountAsync(input.FilterText, input.RequestDate, input.ProtocolId, input.DepartmentId, input.DoctorId);
         
        var items = await radiologyRequestRepository.GetListRadiologyRequestWithNavigationPropertiesAsync(input.FilterText, input.RequestDate, input.ProtocolId, input.DepartmentId, input.DoctorId, input.Sorting, input.MaxResultCount, input.SkipCount);
         
        return new PagedResultDto<RadiologyRequestWithNavigationPropertiesDto>(
            totalCount,
            ObjectMapper.Map<List<RadiologyRequestWithNavigationProperties>, List<RadiologyRequestWithNavigationPropertiesDto>>(items)
        );
    }
    #endregion



    #region UpdateAsync
    [Authorize(HealthCarePermissions.RadiologyRequests.Edit)]
    public virtual async Task<RadiologyRequestDto> UpdateAsync(Guid id, RadiologyRequestUpdateDto input)
    {
        var radiologyRequest = await radiologyRequestManager.UpdateAsync(id, input.RequestDate, input.ProtocolId, input.DepartmentId, input.DoctorId);

        return ObjectMapper.Map<RadiologyRequest, RadiologyRequestDto>(radiologyRequest);
    }
    #endregion

    #region DeleteAsync
    public virtual async Task DeleteAsync(Guid id) => await radiologyRequestRepository.DeleteAsync(id);
    #endregion 

    #region GetDepartmentLookupAsync
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region GetDoctorLookupAsync
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region GetProtocolLookupAsync
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetProtocolLookupAsync(LookupRequestDto input)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region GetListAsExcelFileAsync
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyRequestExcelDownloadDto input)
    {
        var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var radiologyRequests = await radiologyRequestRepository.GetListRadiologyRequestWithNavigationPropertiesAsync(input.FilterText, input.RequestDate, input.ProtocolId, input.DepartmentId, input.DoctorId);
        var items = radiologyRequests.Select(item => new
        {
            item.RadiologyRequest.RequestDate,
            Protocol = item.Protocol.Id,
            Department = item.Department?.Name,
            Doctor = item.Doctor?.FirstName
        });

        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new RemoteStreamContent(memoryStream, "RadiologyRequests.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }
    #endregion

    #region GetDownloadToken
    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new RadiologyRequestDownloadTokenCacheItem { Token = token },
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
