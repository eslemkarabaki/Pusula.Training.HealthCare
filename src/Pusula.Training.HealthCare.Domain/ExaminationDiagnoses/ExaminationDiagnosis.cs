using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses;

public sealed class ExaminationDiagnosis : FullAuditedAggregateRoot<Guid>
{
    public Guid ExaminationId { get; private set; }
    public Guid DiagnosisId { get; private set; }
    public string Explanation { get; private set; }
    public string Type { get; private set; }

    private ExaminationDiagnosis()
    {
        Explanation = string.Empty;
        Type = string.Empty;
    }

    public ExaminationDiagnosis(Guid id, Guid examinationId, Guid diagnosisId, string explanation, string type)
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotDefaultOrNull<Guid>(examinationId, nameof(examinationId));
        Check.NotDefaultOrNull<Guid>(diagnosisId, nameof(diagnosisId));
        Check.NotNullOrWhiteSpace(explanation, nameof(explanation), ExaminationDiagnosisConsts.ExplanationMaxLength);
        Check.NotNullOrWhiteSpace(type, nameof(type), ExaminationDiagnosisConsts.TypeMaxLength);

        Id = id;
        ExaminationId = examinationId;
        DiagnosisId = diagnosisId;
        Explanation = explanation;
        Type = type;
    }
}