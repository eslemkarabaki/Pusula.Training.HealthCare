using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PatientNotes;

public class PatientNoteManager(IPatientNoteRepository patientNoteRepository) : DomainService
{
    public async Task CreateNotesAsync(Guid patientId, IEnumerable<PatientNote> notes)
    {
        if (!notes.Any()) return;

        var addressList = notes.Select(
            e => new PatientNote(GuidGenerator.Create(), patientId, e.Note)
        );
        await patientNoteRepository.InsertManyAsync(addressList);
    }

    private async Task CreateNotesAsync(
        Guid patientId,
        IEnumerable<PatientNote> existingNotes,
        IEnumerable<PatientNote> notes
    )
    {
        var created = notes
            .Where(e => !existingNotes.Any(a => a.Id == e.Id));

        if (!created.Any()) return;

        var addressList = created.Select(
            e => new PatientNote(GuidGenerator.Create(), patientId, e.Note)
        );
        await patientNoteRepository.InsertManyAsync(addressList);
    }

    private async Task UpdateNotesAsync(IEnumerable<PatientNote> existingNotes, IEnumerable<PatientNote> notes)
    {
        var updated = notes
            .Where(e => existingNotes.Any(a => a.Id == e.Id));

        if (!updated.Any()) return;

        var entities = existingNotes.Where(e => updated.Any(u => u.Id == e.Id));
        foreach (var entity in entities)
        {
            var address = notes.First(e => e.Id == entity.Id);
            entity.SetNote(address.Note);
        }

        await patientNoteRepository.UpdateManyAsync(entities);
    }

    private async Task DeleteNotesAsync(IEnumerable<PatientNote> existingNotes, IEnumerable<PatientNote> notes)
    {
        var deleted = existingNotes
                      .Where(e => !notes.Any(a => a.Id == e.Id))
                      .Select(e => e.Id);
        if (!deleted.Any()) return;

        await patientNoteRepository.DeleteManyAsync(deleted);
    }

    public async Task SetNotesAsync(Guid patientId, IEnumerable<PatientNote> notes)
    {
        var existingNotes = await patientNoteRepository.GetListAsync(e => e.PatientId == patientId);
        if (existingNotes.Count == 0)
        {
            await CreateNotesAsync(patientId, notes);
            return;
        }

        if (!notes.Any())
        {
            await patientNoteRepository.DeleteAsync(e => true);
            return;
        }

        await DeleteNotesAsync(existingNotes, notes);
        await UpdateNotesAsync(existingNotes, notes);
        await CreateNotesAsync(patientId, existingNotes, notes);
    }
}