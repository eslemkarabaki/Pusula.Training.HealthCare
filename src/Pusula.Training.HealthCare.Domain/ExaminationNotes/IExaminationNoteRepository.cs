using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.ExaminationNotes;

public interface IExaminationNoteRepository : IRepository<ExaminationNote, Guid>
{
}