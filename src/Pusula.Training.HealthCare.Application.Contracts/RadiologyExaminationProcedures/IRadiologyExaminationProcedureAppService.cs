using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public interface IRadiologyExaminationProcedureAppService : IApplicationService
    {
        Task<RadiologyExaminationProcedureDto> GetAsync(Guid id);
        Task<PagedResultDto<RadiologyExaminationProcedureDto>> GetListAsync(GetRadiologyExaminationProceduresInput input);
        Task<RadiologyExaminationProcedureDto> CreateAsync(RadiologyExaminationProcedureCreateDto input);
        Task<RadiologyExaminationProcedureDto> UpdateAsync(Guid id, RadiologyExaminationProcedureUpdateDto input);
        Task DeleteAsync(Guid id);
        Task DeleteByIdsAsync(List<Guid> RadiologyExaminationProcedureIds);
        Task DeleteAllAsync(GetRadiologyExaminationProceduresInput input);
        Task<IRemoteStreamContent> GetListAsExcelFileAsync(RadiologyExaminationProcedureExcelDownloadDto input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}
