using Pusula.Training.HealthCare.ExaminationsPhysical;
using Pusula.Training.HealthCare.ProtocolTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses;

public interface IExaminationDiagnosisAppService : IApplicationService
{
    Task<ExaminationDiagnosisDto> CreateAsync(ExaminationDiagnosisCreateDto input);
    Task<ExaminationDiagnosisDto> UpdateAsync(Guid id, ExaminationDiagnosisUpdateDto input);
    Task<ExaminationDiagnosisDto> GetAsync(Guid id);
    Task<List<ExaminationDiagnosisDto>> GetListAsync(GetExaminationDiagnosisInput input);
    Task DeleteAsync(Guid id);
}
