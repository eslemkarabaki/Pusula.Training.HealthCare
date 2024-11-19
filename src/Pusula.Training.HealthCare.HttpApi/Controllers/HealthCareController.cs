using Pusula.Training.HealthCare.Localization;
using Pusula.Training.HealthCare.Tests;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Pusula.Training.HealthCare.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HealthCareController : AbpControllerBase
{
    protected HealthCareController()
    {
        LocalizationResource = typeof(HealthCareResource);
    }

    public Task<TestDto> CreateAsync(TestCreateDto input) => throw new NotImplementedException();
}
