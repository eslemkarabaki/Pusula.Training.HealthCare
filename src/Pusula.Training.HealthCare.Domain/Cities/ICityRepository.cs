using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Cities;

public interface ICityRepository : IRepository<City, Guid>
{
}