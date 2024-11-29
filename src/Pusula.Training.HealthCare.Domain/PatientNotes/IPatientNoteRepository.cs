using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.PatientNotes;

public interface IPatientNoteRepository : IRepository<PatientNote, Guid>
{
}