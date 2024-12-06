using Pusula.Training.HealthCare.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public interface IRadiologyRequestItemsAppService : IApplicationService
{
    Task<RadiologyRequestItemDto> GetAsync(Guid id);
    Task<PagedResultDto<RadiologyRequestItemDto>> GetListAsync(GetRadiologyRequestItemsInput input);
    Task<RadiologyRequestItemWithNavigationPropertiesDto> GetNavigationPropertiesAsync(Guid id);
    Task<RadiologyRequestItemDto> CreateAsync(RadiologyRequestItemCreateDto input);
    Task<RadiologyRequestItemDto> UpdateAsync(Guid id, RadiologyRequestItemUpdateDto input);
    Task DeleteAsync(Guid id);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyRequestItemExcelDownloadDto input);
    Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    Task<PagedResultDto<LookupDto<Guid>>> GetRadiologyRequestLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetRadiologyExaminationLookupAsync(LookupRequestDto input);

}