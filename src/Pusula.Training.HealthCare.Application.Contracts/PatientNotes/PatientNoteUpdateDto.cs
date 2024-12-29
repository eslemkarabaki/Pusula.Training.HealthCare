using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.PatientNotes;

public class PatientNoteUpdateDto : EntityDto<Guid> ,IHasConcurrencyStamp
{
    [Required]
    [StringLength(PatientNoteConsts.NoteMaxLength)]
    public string Note { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;
}