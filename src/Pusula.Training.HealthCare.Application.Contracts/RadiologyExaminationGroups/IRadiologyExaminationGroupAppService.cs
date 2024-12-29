using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public interface IRadiologyExaminationGroupAppService : IApplicationService
    {
        Task<PagedResultDto<RadiologyExaminationGroupDto>> GetListAsync(GetRadiologyExaminationGroupsInput input);

        Task<RadiologyExaminationGroupDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<RadiologyExaminationGroupDto> CreateAsync(RadiologyExaminationGroupCreateDto input);

        Task<RadiologyExaminationGroupDto> UpdateAsync(Guid id, RadiologyExaminationGroupUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationGroupExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> RadiologyExaminationGroupIds);

        Task DeleteAllAsync(GetRadiologyExaminationGroupsInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}
