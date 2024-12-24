using System;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Medicines;
using Pusula.Training.HealthCare.PatientHistoryMedicines;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PatientHistories;

public class PatientHistoryManager(
    IPatientHistoryRepository patientHistoryRepository,
    IMedicineRepository medicineRepository
) : DomainService
{
    public async Task AddMedicineAsync(Guid id, Guid medicineId, string? explanation)
    {
        var patientHistory = await patientHistoryRepository.GetAsync(id);
        patientHistory.Medicines.Add(new PatientHistoryMedicine(id, medicineId, explanation));
        await patientHistoryRepository.UpdateAsync(patientHistory);
    }
}