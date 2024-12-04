using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.PatientNotes;

public class PatientNoteDto : FullAuditedEntityDto<Guid>
{
    public Guid PatientId { get; set; }
    public string Note { get; set; } = null!;
}