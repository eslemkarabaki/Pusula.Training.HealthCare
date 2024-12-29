using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PatientTypes;

public class PatientTypeManager(IPatientTypeRepository patientTypeRepository) : DomainService
{
    public async Task<PatientType> CreateAsync(string name)
    {
        var patientType = new PatientType(GuidGenerator.Create(), name);
        return await patientTypeRepository.InsertAsync(patientType);
    }

    public async Task<PatientType> UpdateAsync(Guid id, string name, string? concurrencyStamp = null)
    {
        var patientType = await patientTypeRepository.GetAsync(id);
        patientType.SetName(name);
        patientType.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await patientTypeRepository.UpdateAsync(patientType);
    }
}