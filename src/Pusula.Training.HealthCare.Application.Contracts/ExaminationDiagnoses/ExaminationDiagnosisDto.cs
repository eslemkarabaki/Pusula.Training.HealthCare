using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses;

public class ExaminationDiagnosisDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid Id { get; set; }
    public Guid ExaminationId { get; set; }
    public Guid DiagnosisId { get; set; }
    public string? Explanation { get; set; }
    public string? Type { get; set; }
    public string ConcurrencyStamp { get; set; } = null!;
}
