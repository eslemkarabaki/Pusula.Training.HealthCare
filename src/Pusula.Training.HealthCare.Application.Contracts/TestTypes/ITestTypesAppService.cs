using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.TestTypes;

public interface ITestTypesAppService : IApplicationService
{
    Task<PagedResultDto<TestTypeDto>> GetListAsync(GetTestTypesInput input);
    Task<TestTypeDto> GetAsync(Guid id);
    Task<TestTypeDto> CreateAsync(TestTypeCreateDto input);
    Task<TestTypeDto> UpdateAsync(Guid id, TestTypeUpdateDto input);
    Task DeleteAsync(Guid id);
    Task DeleteByIdsAsync(List<Guid> testTypeIds);
    Task DeleteAllAsync(GetTestTypesInput input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(TestTypeExcelDownloadDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}
