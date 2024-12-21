using Localization.Resources.AbpUi;
using Pusula.Training.HealthCare.Localization;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp;
using Microsoft.AspNetCore.Builder;
using Pusula.Training.HealthCare.Hub;

namespace Pusula.Training.HealthCare;

[DependsOn(
    typeof(HealthCareApplicationContractsModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule)
    )]
[DependsOn(typeof(AbpAspNetCoreSignalRModule))]
    public class HealthCareHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();

        app.UseRouting(); // Ensure this is called first

        app.UseAuthentication(); // Ensure authentication middleware is added
        app.UseAuthorization();  // Ensure authorization middleware is added

        app.UseAntiforgery(); // Add this line between UseRouting and UseEndpoints

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<NotificationHub>("/notification-hub");
        });
    }


    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {   
            options.Resources
                .Get<HealthCareResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });

        Configure<AbpExceptionHandlingOptions>(options =>
        {
            options.SendExceptionsDetailsToClients = true; // Sends detailed exceptions
            options.SendStackTraceToClients = true;        // Sends stack trace to clients
        });
    }
}
