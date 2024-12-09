using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Diagnoses;

public interface IDiagnosisAppService : IApplicationService
{
    Task<DiagnosisDto> GetAsync(Guid id);
    Task<PagedResultDto<DiagnosisDto>> GetListAsync(GetDiagnosisInput input);
    Task<DiagnosisDto> CreateAsync(DiagnosisCreateDto input);
    Task<DiagnosisDto> UpdateAsync(Guid id, DiagnosisUpdateDto input);
    Task DeleteAsync(Guid id);
}
