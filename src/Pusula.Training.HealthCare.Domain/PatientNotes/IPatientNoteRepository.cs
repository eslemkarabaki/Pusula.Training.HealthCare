using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.PatientNotes;

public interface IPatientNoteRepository : IRepository<PatientNote, Guid>
{
}