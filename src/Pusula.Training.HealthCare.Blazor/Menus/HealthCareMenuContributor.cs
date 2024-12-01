using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pusula.Training.HealthCare.Localization;
using Pusula.Training.HealthCare.MultiTenancy;
using Pusula.Training.HealthCare.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Pusula.Training.HealthCare.Blazor.Menus;

public class HealthCareMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<HealthCareResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                HealthCareMenus.Home,
                l["Menu:Home"],
                "/",
                "fas fa-home",
                0
            )
        );

        ConfigureTenantMenu(administration, MultiTenancyConsts.IsEnabled);

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Patients,
                l["Menu:Patients"],
                "/patients",
                "fas fa-hospital-user",
                requiredPermissionName: HealthCarePermissions.Patients.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Departments,
                l["Menu:Departments"],
                "/departments",
                "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Departments.Default)

        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Hospitals,
                l["Menu:Hospitals"],
                url: "/hospitals",
                icon: "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Hospitals.Default)
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Appointments,
                l["Menu:Appointments"],
                icon: "fa fa-calendar-check"

        )
            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.AppointmentTypes,
                                l["Appointment Definition"],
                                "/appointment-type",
                                "fa-solid fa-font", 
                                requiredPermissionName: HealthCarePermissions.AppointmentTypes.Default)
            )

            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.Transactions,
                l["Transactions"],
                "/appointments",
                "fa-solid fa-pen-to-square", 
                requiredPermissionName: HealthCarePermissions.Appointments.Default)
            )
            );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                    HealthCareMenus.Locations,
                    l["Menu:Locations"],
                    icon: "fas fa-compass"
                )
                .AddItem(new ApplicationMenuItem(
                        HealthCareMenus.Countries,
                        l["Menu:Countries"],
                        "/countries",
                        "fas fa-flag",
                        requiredPermissionName: HealthCarePermissions.Countries.Default
                    )
                )
                .AddItem(new ApplicationMenuItem(
                        HealthCareMenus.Cities,
                        l["Menu:Cities"],
                        "/cities",
                        "fas fa-city",
                        requiredPermissionName: HealthCarePermissions.Cities.Default
                    )
                )
                .AddItem(new ApplicationMenuItem(
                        HealthCareMenus.Districts,
                        l["Menu:Districts"],
                        "/districts",
                        "fa fa-file-alt",
                        requiredPermissionName: HealthCarePermissions.Districts.Default
                    )
                )

        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Examinations,
                l["Menu:Examinations"],
                url: "/examinations",
                icon: "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Examinations.Default)
        );

        return Task.CompletedTask;
    }

    private static void ConfigureTenantMenu(ApplicationMenuItem? item, bool isMultiTenancyEnabled)
    {
        if (isMultiTenancyEnabled)
        {
            item?.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            item?.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
    }
}