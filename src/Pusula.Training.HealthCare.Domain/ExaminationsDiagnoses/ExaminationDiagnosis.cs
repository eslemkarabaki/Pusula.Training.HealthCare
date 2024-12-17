using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses;

public sealed class ExaminationDiagnosis : Entity<Guid>
{
    public Guid ExaminationId { get;  set; }
    public Guid DiagnosisId { get;  set; }
    public string Explanation { get;  set; }
    public string Type { get;  set; }

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