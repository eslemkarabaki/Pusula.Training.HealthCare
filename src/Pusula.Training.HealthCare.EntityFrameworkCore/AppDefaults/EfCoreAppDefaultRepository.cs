using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Pusula.Training.HealthCare.AppDefaults;

public class EfCoreAppDefaultRepository(IDbContextProvider<HealthCareDbContext> dbContextProvider)
    : EfCoreRepository<HealthCareDbContext, AppDefault, Guid>(dbContextProvider), IAppDefaultRepository
{
    public async Task<Country?> GetCurrentCountryAsync()
    {
        var dbContext = await GetDbContextAsync();
        var query =
            from appDefault in dbContext.AppDefaults
            join country in dbContext.Countries
                on appDefault.CurrentCountryId equals country.Id
            where appDefault.CurrentCountryId == country.Id
            select country;
        return await query.FirstOrDefaultAsync();
    }
}