using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Pusula.Training.HealthCare.Shared;

namespace Pusula.Training.HealthCare.Examinations;

public interface IExaminationAppService : IApplicationService
{
    Task<PagedResultDto<ExaminationDto>> GetListAsync(GetExaminationsInput input);
    Task<ExaminationDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<ExaminationDto> CreateAsync(ExaminationCreateDto input);
    Task<ExaminationDto> UpdateAsync(Guid id, ExaminationUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(ExaminationExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> examinationsIds);
    Task DeleteAllAsync(GetExaminationsInput input);
    Task<DownloadTokenResultDto> GetDownloadTokenAsync();
}