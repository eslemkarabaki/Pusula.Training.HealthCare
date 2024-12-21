using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Medicines;

public interface IMedicineRepository : IRepository<Medicine, Guid>
{
}