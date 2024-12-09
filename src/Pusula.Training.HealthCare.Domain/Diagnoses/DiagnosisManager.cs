using Pusula.Training.HealthCare.GlobalExceptions;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Diagnoses;

public class DiagnosisManager : DomainService
{
    private readonly IDiagnosisRepository _diagnosisRepository;
    public DiagnosisManager(IDiagnosisRepository diagnosisRepository)
    {
        _diagnosisRepository = diagnosisRepository;
    }
    public async Task<Diagnosis> CreateAsync(string code, string name)
    {
        var existingDiagnosis = await _diagnosisRepository.FindByCodeAsync(code);
        GlobalException.ThrowIf(existingDiagnosis != null, "Diagnosis:DuplicateCode");
       return await _diagnosisRepository.InsertAsync(new Diagnosis(GuidGenerator.Create(),code, name));
    }


    public async Task<Diagnosis> UptadeAsync(Guid id, string code, string name)
    {
        var diagnosis = await _diagnosisRepository.GetAsync(id);
        diagnosis.SetCode(code);
        diagnosis.SetName(name);
        return await _diagnosisRepository.UpdateAsync(diagnosis);
    }


}
