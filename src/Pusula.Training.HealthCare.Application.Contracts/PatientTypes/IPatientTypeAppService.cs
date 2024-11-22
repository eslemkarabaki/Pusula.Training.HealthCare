using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.PatientTypes;

public interface
    IPatientTypeAppService : IApplicationService
{
    Task<List<PatientTypeDto>> GetListAsync();
    Task<PatientTypeDto> CreateAsync(PatientTypeUpdateDto input);
    Task<PatientTypeDto> UpdateAsync(Guid id, PatientTypeUpdateDto input);
    Task DeleteAsync(Guid id);
}