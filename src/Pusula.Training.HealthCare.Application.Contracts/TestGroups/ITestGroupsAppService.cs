using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.TestGroups;

public interface ITestGroupsAppService : IApplicationService
{
    Task<PagedResultDto<TestGroupDto>> GetListAsync(GetTestGroupsInput input);

    Task<TestGroupDto> GetAsync(Guid id);

    Task<TestGroupDto> CreateAsync(TestGroupCreateDto input);

    Task<TestGroupDto> UpdateAsync(Guid id, TestGroupUpdateDto input);

    Task DeleteAsync(Guid id);

    Task DeleteByIdsAsync(List<Guid> testGroupIds);

    Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestGroupExcelDownloadDto input);

    Task<DownloadTokenResultDto> GetDownloadTokenAsync();
}
