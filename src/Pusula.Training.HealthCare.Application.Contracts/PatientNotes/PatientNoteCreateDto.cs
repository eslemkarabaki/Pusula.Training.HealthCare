using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.PatientNotes;

public class PatientNoteCreateDto : EntityDto<Guid>
{
    [Required]
    [StringLength(PatientNoteConsts.NoteMaxLength)]
    public string Note { get; set; } = null!;
}