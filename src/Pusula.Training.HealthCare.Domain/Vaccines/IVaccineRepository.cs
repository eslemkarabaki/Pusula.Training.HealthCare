using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Vaccines;

public interface IVaccineRepository : IRepository<Vaccine, Guid>
{
}