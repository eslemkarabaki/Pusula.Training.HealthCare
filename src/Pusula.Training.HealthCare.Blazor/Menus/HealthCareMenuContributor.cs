using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Pusula.Training.HealthCare.Localization;
using Pusula.Training.HealthCare.MultiTenancy;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.ProtocolTypes;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

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
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

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

        if (currentUser.IsInRole(HealthCareRoles.Doctor))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    HealthCareMenus.Medical,
                    l["Menu:Medical"],
                    "/medical",
                    "fa fa-file-alt"
                )
            );
        }

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Doctors,
                l["Menu:Doctors"],
                "/doctors",
                "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Doctors.Menu
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Departments,
                l["Menu:Departments"],
                "/departments",
                "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Departments.Menu
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Hospitals,
                l["Menu:Hospitals"],
                "/hospitals",
                "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Hospitals.Menu
            )
        );

#region Appoinments

        context.Menu.AddItem(
            new ApplicationMenuItem(
                    HealthCareMenus.Appointments,
                    l["Menu:Appointments"],
                    icon: "fa fa-calendar-check"
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.AppointmentTypes,
                        l["Appointment Type"],
                        "/appointment-type",
                        requiredPermissionName: HealthCarePermissions.AppointmentTypes.Menu
                    )
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.Transactions,
                        l["Appointment"],
                        "/appointments",
                        requiredPermissionName: HealthCarePermissions.Appointments.Menu
                    )
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.Reports,
                        l["Reports"],
                        "/appointment-reports",
                        requiredPermissionName: HealthCarePermissions.Appointments.Menu
                    )
                )
        );

#endregion

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Diagnoses,
                l["Menu:Diagnosis"],
                "/diagnosis",
                "fa fa-file-alt",
                requiredPermissionName: HealthCarePermissions.Diagnosis.Menu
            )
        );

        #region Radiologies
        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Radiologies,
                l["Menu:Radiologies"],
                icon: "fa fa-calendar-check"
            )
            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.RadiologyDefinitions,
                l["Radiology Definition"],
                "/radiology-definitions",
                requiredPermissionName: HealthCarePermissions.RadiologyDefinitions.Default)
            )
            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.RadiologyTransactions,
                l["Radiology Transactions"],
                "/radiology-transaction",
                requiredPermissionName: HealthCarePermissions.RadiologyTransactions.Default)
            )
            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.RadiologyReports,
                l["Reports"],
                "/radiology-reports",
                requiredPermissionName: HealthCarePermissions.RadiologyReports.Default)
            )
            .AddItem(new ApplicationMenuItem(
                HealthCareMenus.RadiologyExaminationRequests,
                l["Radiology Requests"],
                "/radiology-requests",
                requiredPermissionName: HealthCarePermissions.RadiologyRequestItems.Default)
            )
        );
        #endregion

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
                                requiredPermissionName: HealthCarePermissions.ProtocolTypes.Menu
                            )
                        )
                        .AddItem(
                            new ApplicationMenuItem(
                                HealthCareMenus.Insurances,
                                l["Menu:Insurances"],
                                "/prm/definitions/insurances",
                                requiredPermissionName: HealthCarePermissions.Insurances.Menu
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
                                        requiredPermissionName: HealthCarePermissions.Countries.Menu
                                    )
                                )
                                .AddItem(
                                    new ApplicationMenuItem(
                                        HealthCareMenus.Cities,
                                        l["Menu:Cities"],
                                        "prm/definitions/cities",
                                        requiredPermissionName: HealthCarePermissions.Cities.Menu
                                    )
                                )
                                .AddItem(
                                    new ApplicationMenuItem(
                                        HealthCareMenus.Districts,
                                        l["Menu:Districts"],
                                        "prm/definitions/districts",
                                        requiredPermissionName: HealthCarePermissions.Districts.Menu
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
                            requiredPermissionName: HealthCarePermissions.Patients.Menu
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
                            requiredPermissionName: HealthCarePermissions.PatientProtocols.Menu
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