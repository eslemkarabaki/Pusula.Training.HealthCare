using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.PatientTypes;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.PatientTypes.Default)]
public class PatientTypeAppService(IPatientTypeRepository patientTypeRepository, PatientTypeManager patientTypeManager)
    : HealthCareAppService, IPatientTypeAppService
{
    public async Task<List<PatientTypeDto>> GetListAsync() =>
        ObjectMapper.Map<List<PatientType>, List<PatientTypeDto>>(
            await patientTypeRepository.GetListAsync()
        );

    [Authorize(HealthCarePermissions.PatientTypes.Create)]
    public async Task<PatientTypeDto> CreateAsync(PatientTypeUpdateDto input) =>
        ObjectMapper.Map<PatientType, PatientTypeDto>(await patientTypeManager.CreateAsync(input.Name));

    [Authorize(HealthCarePermissions.PatientTypes.Edit)]
    public async Task<PatientTypeDto> UpdateAsync(Guid id, PatientTypeUpdateDto input) =>
        ObjectMapper.Map<PatientType, PatientTypeDto>(await patientTypeManager.UpdateAsync(id, input.Name));

    [Authorize(HealthCarePermissions.PatientTypes.Delete)]
    public async Task DeleteAsync(Guid id) => await patientTypeRepository.DeleteAsync(id);
}