using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.PatientTypes;

public interface IPatientTypeRepository : IRepository<PatientType, Guid>
{
}