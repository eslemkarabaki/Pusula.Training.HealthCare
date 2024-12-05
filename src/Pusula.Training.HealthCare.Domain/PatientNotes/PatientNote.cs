using System;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.PatientNotes;

public sealed class PatientNote : FullAuditedAggregateRootWithUser<Guid,IdentityUser>
{
    public Guid PatientId { get; private set; }
    public string Note { get; private set; }

    
    protected PatientNote() => Note = string.Empty;

    public PatientNote(Guid id, Guid patientId, string note) : base(id)
    {
        SetPatientId(patientId);
        SetNote(note);
    }

    public void SetPatientId(Guid patientId) => PatientId = Check.NotDefaultOrNull<Guid>(patientId, nameof(patientId));

    public void SetNote(string note) =>
        Note = Check.NotNullOrWhiteSpace(note, nameof(note), PatientNoteConsts.NoteMaxLength);
}