using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.AppDefaults;
using Pusula.Training.HealthCare.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Pusula.Training.HealthCare.Blazor;

public abstract class HealthCareComponentBase : AbpComponentBase
{
    private IAppDefaultAppService? _appDefaultAppService;
    protected IAppDefaultAppService AppDefaultAppService => LazyGetRequiredService(ref _appDefaultAppService)!;
    protected HealthCareComponentBase() => LocalizationResource = typeof(HealthCareResource);
}