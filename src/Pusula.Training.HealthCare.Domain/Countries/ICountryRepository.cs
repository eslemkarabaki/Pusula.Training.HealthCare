using System;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Countries;

public interface ICountryRepository : IRepository<Country, Guid>
{
}