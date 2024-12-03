using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.ExaminationNotes;

public sealed class ExaminationNote : FullAuditedAggregateRoot<Guid>
{
    public Guid ExaminationId { get; private set; }
    public Guid DoctorId { get; private set; }
    public string Note { get; private set; }


    private ExaminationNote()
    {
        Note = string.Empty;
    }

    public ExaminationNote(Guid id, Guid examinationId, Guid doctorId, string note)
    {
        Check.NotDefaultOrNull<Guid>(id, nameof(id));
        Check.NotDefaultOrNull<Guid>(examinationId, nameof(examinationId));
        Check.NotDefaultOrNull<Guid>(doctorId, nameof(doctorId));
        Check.NotNullOrWhiteSpace(note, nameof(note));

        Id = id;
        ExaminationId = examinationId;
        DoctorId = doctorId;
        Note = note;
    }
}