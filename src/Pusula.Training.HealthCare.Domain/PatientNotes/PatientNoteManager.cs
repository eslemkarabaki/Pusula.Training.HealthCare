using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.PatientNotes;

public class PatientNoteManager(IPatientNoteRepository patientNoteRepository) : DomainService
{
    public async Task<PatientNote> CreateNoteAsync(Guid patientId, string note) =>
        await patientNoteRepository.InsertAsync(new PatientNote(GuidGenerator.Create(), patientId, note));

    public async Task<PatientNote> UpdateNoteAsync(Guid id, Guid patientId, string note)
    {
        var patientNote = await patientNoteRepository.GetAsync(id);
        patientNote.SetNote(note);
        return await patientNoteRepository.UpdateAsync(patientNote);
    }
}