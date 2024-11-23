using System;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.AppDefaults;

public interface IAppDefaultRepository : IRepository<AppDefault, Guid>
{
    Task<Country?> GetCurrentCountryAsync();
}