using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public interface IRadiologyExaminationAppService : IApplicationService
    {
        Task<PagedResultDto<RadiologyExaminationDto>> GetListAsync(GetRadiologyExaminationsInput input);

        Task<RadiologyExaminationDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<RadiologyExaminationDto> CreateAsync(RadiologyExaminationCreateDto input);

        Task<RadiologyExaminationDto> UpdateAsync(Guid id, RadiologyExaminationUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> RadiologyExaminationIds);

        Task DeleteAllAsync(GetRadiologyExaminationsInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
