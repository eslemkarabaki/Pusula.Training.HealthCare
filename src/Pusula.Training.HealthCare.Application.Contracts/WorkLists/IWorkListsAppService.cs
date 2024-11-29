using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.WorkLists;

public interface IWorkListsAppService : IApplicationService
{
    Task<PagedResultDto<WorkListDto>> GetListAsync(GetWorkListsInput input);
    Task<WorkListDto> GetAsync(Guid id);
    Task<WorkListDto> CreateAsync(WorkListCreateDto input);
    Task<WorkListDto> UpdateAsync(Guid id, WorkListUpdateDto input);
    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> ids);
    Task DeleteAllAsync(GetWorkListsInput input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(WorkListExcelDownloadDto input);
    Task<DownloadTokenResultDto> GetDownloadTokenAsync();
}
