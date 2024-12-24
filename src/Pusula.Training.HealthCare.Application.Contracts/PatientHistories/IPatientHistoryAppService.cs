using System.Threading.Tasks;
using Pusula.Training.HealthCare.PatientHistoryMedicines;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.PatientHistories;

public interface IPatientHistoryAppService : IApplicationService
{
    Task AddMedicineAsync(PatientHistoryMedicineCreateDto input);

    Task<PatientHistoryDto> GetAsync(GetPatientHistoryInput input);
}