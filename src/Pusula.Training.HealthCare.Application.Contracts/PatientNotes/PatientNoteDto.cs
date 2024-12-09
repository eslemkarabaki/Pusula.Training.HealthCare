using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.PatientNotes;

public class PatientNoteDto : FullAuditedEntityDto<Guid>
{
    public Guid PatientId { get; set; }
    public string Note { get; set; } = null!;
    public string CreatorName { get; set; } = null!;
}