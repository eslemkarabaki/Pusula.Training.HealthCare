using Pusula.Training.HealthCare.Shared;
using System; 
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadiologyRequests;
public interface IRadiologyRequestsAppService : IApplicationService
{
    Task<RadiologyRequestDto> GetAsync(Guid id);
    Task<PagedResultDto<RadiologyRequestDto>> GetListAsync(GetRadiologyRequestsInput input);
    Task<PagedResultDto<RadiologyRequestWithNavigationPropertiesDto>> GetListNavigationPropertiesAsync(GetRadiologyRequestsInput input);
    Task<RadiologyRequestWithNavigationPropertiesDto> GetNavigationPropertiesAsync(Guid id);
    Task<RadiologyRequestDto> CreateAsync(RadiologyRequestCreateDto input);
    Task<RadiologyRequestDto> UpdateAsync(Guid id, RadiologyRequestUpdateDto input);
    Task DeleteAsync(Guid id);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyRequestExcelDownloadDto input);
    Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetProtocolLookupAsync(LookupRequestDto input);
}
