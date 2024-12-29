using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.PatientNotes;

public class PatientNoteDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid PatientId { get; set; }
    public string Note { get; set; } = null!;
    public string CreatorName { get; set; } = null!;
    public string ConcurrencyStamp { get; set; } = null!;
}