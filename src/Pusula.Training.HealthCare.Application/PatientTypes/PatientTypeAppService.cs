using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.PatientTypes;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Patients.Default)] // todo:permission
public class PatientTypeAppService(IPatientTypeRepository patientTypeRepository, PatientTypeManager patientTypeManager)
    : HealthCareAppService, IPatientTypeAppService
{
    public async Task<List<PatientTypeDto>> GetListAsync()
    {
        return ObjectMapper.Map<List<PatientType>, List<PatientTypeDto>>(
            await patientTypeRepository.GetListAsync(false));
    }

    public async Task<PatientTypeDto> CreateAsync(PatientTypeUpdateDto input)
    {
        return ObjectMapper.Map<PatientType, PatientTypeDto>(await patientTypeManager.CreateAsync(input.Name));
    }

    public async Task<PatientTypeDto> UpdateAsync(Guid id, PatientTypeUpdateDto input)
    {
        return ObjectMapper.Map<PatientType, PatientTypeDto>(await patientTypeManager.UpdateAsync(id, input.Name));
    }

    public async Task DeleteAsync(Guid id)
    {
        await patientTypeRepository.DeleteAsync(id);
    }
}