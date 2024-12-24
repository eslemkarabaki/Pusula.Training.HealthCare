using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.PatientHistoryMedicines;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp;

namespace Pusula.Training.HealthCare.PatientHistories;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.PatientHistory.Default)]
public class PatientHistoryAppService(
    IPatientHistoryRepository patientHistoryRepository,
    PatientHistoryManager patientHistoryManager
)
    : HealthCareAppService, IPatientHistoryAppService
{
    public async Task AddMedicineAsync(PatientHistoryMedicineCreateDto input) =>
        await patientHistoryManager.AddMedicineAsync(
            input.PatientHistoryId, input.MedicineId!.Value, input.Explanation
        );

    public async Task<PatientHistoryDto> GetAsync(GetPatientHistoryInput input)
    {
        var items = await patientHistoryRepository.GetAsync(input.PatientHistoryId, input.PatientId);
        return ObjectMapper.Map<PatientHistory, PatientHistoryDto>(items);
    }
}