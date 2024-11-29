using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public interface ITestProcessesAppService : IApplicationService
    {
        Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input);
        Task<TestProcessDto> GetAsync(Guid id);
        Task<TestProcessDto> CreateAsync(TestProcessCreateDto input);
        Task<TestProcessDto> UpdateAsync(Guid id, TestProcessUpdateDto input);
        Task DeleteAsync(Guid id);
        Task DeleteByIdsAsync(List<Guid> testProcessIds);
        Task DeleteAllAsync(GetTestProcessesInput input);
        Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestProcessExcelDownloadDto input);
        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
