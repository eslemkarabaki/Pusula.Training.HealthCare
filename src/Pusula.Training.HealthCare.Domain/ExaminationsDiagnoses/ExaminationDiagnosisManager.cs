using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.GlobalExceptions;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses;

public class ExaminationDiagnosisManager : DomainService
{
    private readonly IExaminationDiagnosisRepository _repository;

    public ExaminationDiagnosisManager(IExaminationDiagnosisRepository repository)
    {
        _repository = repository;
    }
  
    public async Task<ExaminationDiagnosis> CreateAsync(Guid examinationId, Guid diagnosisId, string explanation, string type)
    {

       
        Check.NotDefaultOrNull<Guid>(examinationId, nameof(examinationId));
        Check.NotDefaultOrNull<Guid>(diagnosisId, nameof(diagnosisId));
        Check.NotNullOrWhiteSpace(explanation, nameof(explanation));
        Check.NotNullOrWhiteSpace(type, nameof(type));

        var diagnosis = new ExaminationDiagnosis(GuidGenerator.Create(), examinationId, diagnosisId, explanation, type);

        return await _repository.InsertAsync(diagnosis);
    }
    public async Task<ExaminationDiagnosis> UpdateAsync(
            Guid examinationId,
            Guid diagnosisId,
            string explanation,
            string type)
    {
        var examinationDiagnosis = await _repository.GetAsync(diagnosisId);
        GlobalException.ThrowIf(examinationDiagnosis is null, "Examination Diagnosis is null", "ExaminationDiagnosisCode");

        examinationDiagnosis.ExaminationId = examinationId;
        examinationDiagnosis.DiagnosisId = diagnosisId;
        examinationDiagnosis.Explanation = explanation;
        examinationDiagnosis.Type = type;

        return await _repository.UpdateAsync(examinationDiagnosis);
    }


}
