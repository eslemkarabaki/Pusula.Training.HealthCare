using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ExaminationNotes;

public class ExaminationNote : FullAuditedAggregateRoot<Guid>
{
    public Guid ExaminationId { get; private set; }
    public Guid DoctorId { get; private set; }
    public string Note { get; private set; }

    protected ExaminationNote() => Note = string.Empty;

    public ExaminationNote(Guid id, Guid examinationId, Guid doctorId, string note) : base(id)
    {
        SetExaminationId(examinationId);
        SetDoctorId(doctorId);
        SetNote(note);
    }

    public void SetExaminationId(Guid examinationId) =>
        ExaminationId = Check.NotDefaultOrNull<Guid>(examinationId, nameof(examinationId));

    public void SetDoctorId(Guid doctorId) => DoctorId = Check.NotDefaultOrNull<Guid>(doctorId, nameof(doctorId));
    public void SetNote(string note) => Note = Check.NotNullOrWhiteSpace(note, nameof(note));
}