using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Pusula.Training.HealthCare.Localization;
using Pusula.Training.HealthCare.MultiTenancy;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.ProtocolTypes;
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

        ConfigurePatientRegistrationMenu(context, l);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Doctors,
                l["Menu:Doctors"],
                "/doctors",
                "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Doctors.Default
            )
        );
        
        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Departments,
                l["Menu:Departments"],
                "/departments",
                "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Departments.Default
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Hospitals,
                l["Menu:Hospitals"],
                url: "/hospitals",
                icon: "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Hospitals.Default)
        );

        #region Appoinments
        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Appointments,
                l["Menu:Appointments"],
                icon: "fa fa-calendar-check"

        )
            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.AppointmentTypes,
                                l["Appointment Type"],
                                "/appointment-type",
                                requiredPermissionName: HealthCarePermissions.AppointmentTypes.Default)
            )

            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.Transactions,
                l["Appointment"],
                "/appointments",
                requiredPermissionName: HealthCarePermissions.Appointments.Default)
            )
            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.Reports,
                                l["Reports"],
                                "/appointment-reports",
                                requiredPermissionName: HealthCarePermissions.Appointments.Default)
            )
            );
        #endregion
        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Diagnoses,
                l["Menu:Diagnosis"],
                url: "/diagnosis",
                icon: "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Diagnosis.Default)
        );
        return Task.CompletedTask;
    }

    private void ConfigurePatientRegistrationMenu(MenuConfigurationContext context, IStringLocalizer l) =>
        context.Menu.AddItem(
            new ApplicationMenuItem(
                    HealthCareMenus.PatientRegistration,
                    l["Menu:PatientRegistration"],
                    icon: "fas fa-hospital-user"
                )
                .AddItem(
                    new ApplicationMenuItem(
                            HealthCareMenus.PatientRegistrationDefinitions,
                            l["Menu:Definitions"]
                        )
                        .AddItem(
                            new ApplicationMenuItem(
                                HealthCareMenus.ProtocolTypes,
                                l["Menu:ProtocolTypes"],
                                "/prm/definitions/protocol-types",
                                requiredPermissionName: HealthCarePermissions.ProtocolTypes.Default
                            )
                        )
                        .AddItem(
                            new ApplicationMenuItem(
                                HealthCareMenus.Insurances,
                                l["Menu:Insurances"],
                                "/prm/definitions/insurances",
                                requiredPermissionName: HealthCarePermissions.Insurances.Default
                            )
                        )
                        .AddItem(
                            new ApplicationMenuItem(
                                    HealthCareMenus.Locations,
                                    l["Menu:Address"]
                                )
                                .AddItem(
                                    new ApplicationMenuItem(
                                        HealthCareMenus.Countries,
                                        l["Menu:Countries"],
                                        "prm/definitions/countries",
                                        requiredPermissionName: HealthCarePermissions.Countries.Default
                                    )
                                )
                                .AddItem(
                                    new ApplicationMenuItem(
                                        HealthCareMenus.Cities,
                                        l["Menu:Cities"],
                                        "prm/definitions/cities",
                                        requiredPermissionName: HealthCarePermissions.Cities.Default
                                    )
                                )
                                .AddItem(
                                    new ApplicationMenuItem(
                                        HealthCareMenus.Districts,
                                        l["Menu:Districts"],
                                        "prm/definitions/districts",
                                        requiredPermissionName: HealthCarePermissions.Districts.Default
                                    )
                                )
                        )
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.PatientRegistrationManagement,
                        l["Menu:Management"]
                    ).AddItem(
                        new ApplicationMenuItem(
                            HealthCareMenus.Patients,
                            l["Menu:Patients"],
                            "/prm/management/patients",
                            requiredPermissionName: HealthCarePermissions.Patients.Default
                        )
                    )
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.PatientRegistrationReports,
                        l["Menu:Reports"]
                    ).AddItem(
                        new ApplicationMenuItem(
                            HealthCareMenus.PatientProtocols,
                            l["Menu:PatientProtocols"],
                            "/prm/reports/patient-protocols",
                            requiredPermissionName: HealthCarePermissions.PatientProtocols.Default
                        )
                    )
                )
        );

    private static void ConfigureTenantMenu(ApplicationMenuItem? item, bool isMultiTenancyEnabled)
    {
        if (isMultiTenancyEnabled)
        {
            item?.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        } else
        {
            item?.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }
    }
}