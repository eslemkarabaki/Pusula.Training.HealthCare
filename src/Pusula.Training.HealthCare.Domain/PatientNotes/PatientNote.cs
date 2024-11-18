using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.PatientNotes;

public sealed class PatientNote : FullAuditedEntity<Guid>
{
    public Guid PatientId { get; private set; }
    public string Note { get; private set; }

    private PatientNote()
    {
        Note = string.Empty;
    }

    internal PatientNote(Guid id, Guid patientId, string note) : base(id)
    {
        Set(patientId, note);
    }

    internal void Set(Guid patientId, string note)
    {
        PatientId = Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));
        Note = Check.NotNullOrWhiteSpace(note, nameof(note), PatientNoteConsts.NoteMaxLength);
    }
}