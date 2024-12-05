using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.PatientNotes;

public class PatientNoteCreateDto
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(PatientNoteConsts.NoteMaxLength)]
    public string Note { get; set; } = null!;
}