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

#region Appoinments

        context.Menu.AddItem(
            new ApplicationMenuItem(
                    HealthCareMenus.Appointments,
                    l["Menu:Appointments"],
                    icon: "fa fa-calendar-check"
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.Transactions,
                        l["Menu:Appointment"],
                        "/appointments",
                        requiredPermissionName: HealthCarePermissions.Appointments.Menu
                    )
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.Reports,
                        l["Menu:Reports"],
                        "/appointment-reports",
                        requiredPermissionName: HealthCarePermissions.Appointments.Menu
                    )
                )
        );

#endregion

#region Radiologies

        context.Menu.AddItem(
            new ApplicationMenuItem(
                    HealthCareMenus.Radiologies,
                    l["Menu:Radiologies"],
                    icon: "fa fa-calendar-check"
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.RadiologyDefinitions,
                        l["Definition"],
                        "/radiology-definitions",
                        requiredPermissionName: HealthCarePermissions.RadiologyDefinitions.Default
                    )
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.RadiologyTransactions,
                        l["Transactions"],
                        "/radiology-transaction",
                        requiredPermissionName: HealthCarePermissions.RadiologyTransactions.Default
                    )
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.RadiologyReports,
                        l["Reports"],
                        "/radiology-reports",
                        requiredPermissionName: HealthCarePermissions.RadiologyReports.Default
                    )
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.RadiologyExaminationRequests,
                        l["Requests"],
                        "/radiology-requests",
                        requiredPermissionName: HealthCarePermissions.RadiologyRequestItems.Default
                    )
                )
        );

#endregion

        context.Menu.AddItem(
            new ApplicationMenuItem(
                HealthCareMenus.Doctors,
                l["Menu:Doctors"],
                "/definition/doctors",
                "fas fa-user-md",
                requiredPermissionName: HealthCarePermissions.Doctors.Menu
            )
        );

        ConfigureDefinitionMenu(context, l);
        
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

    private void ConfigureDefinitionMenu(MenuConfigurationContext context, IStringLocalizer l)=>
        context.Menu.AddItem(
            new ApplicationMenuItem(
                    HealthCareMenus.Definitions,
                    l["Menu:Definitions"],
                    icon: "fa fa-file-alt"
                )
                .AddItem(
                    new ApplicationMenuItem(
                        HealthCareMenus.Diagnoses,
                        l["Menu:Diagnosis"],
                        "/definition/diagnosis",
                        requiredPermissionName: HealthCarePermissions.Diagnosis.Menu
                    )
                ).AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Departments,
                         l["Menu:Departments"],
                         "/definition/departments",
                         requiredPermissionName: HealthCarePermissions.Departments.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Hospitals,
                         l["Menu:Hospitals"],
                         "/definition/hospitals",
                         requiredPermissionName: HealthCarePermissions.Hospitals.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.AppointmentTypes,
                         l["Menu:AppointmentType"],
                         "/definition/appointment-type",
                         requiredPermissionName: HealthCarePermissions.AppointmentTypes.Menu
                     )
                 ).AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.ProtocolTypes,
                         l["Menu:ProtocolTypes"],
                         "/definition/protocol-types",
                         requiredPermissionName: HealthCarePermissions.ProtocolTypes.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Insurances,
                         l["Menu:Insurances"],
                         "/definition/insurances",
                         requiredPermissionName: HealthCarePermissions.Insurances.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                             HealthCareMenus.Address,
                             l["Menu:Address"]
                         )
                         .AddItem(
                             new ApplicationMenuItem(
                                 HealthCareMenus.Countries,
                                 l["Menu:Countries"],
                                 "/definition/countries",
                                 requiredPermissionName: HealthCarePermissions.Countries.Menu
                             )
                         )
                         .AddItem(
                             new ApplicationMenuItem(
                                 HealthCareMenus.Cities,
                                 l["Menu:Cities"],
                                 "/definition/cities",
                                 requiredPermissionName: HealthCarePermissions.Cities.Menu
                             )
                         )
                         .AddItem(
                             new ApplicationMenuItem(
                                 HealthCareMenus.Districts,
                                 l["Menu:Districts"],
                                 "/definition/districts",
                                 requiredPermissionName: HealthCarePermissions.Districts.Menu
                             )
                         )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Allergies,
                         l["Menu:Allergies"],
                         "/definition/allergies",
                         requiredPermissionName: HealthCarePermissions.Allergies.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Medicines,
                         l["Menu:Medicines"],
                         "/definition/medicines",
                         requiredPermissionName: HealthCarePermissions.Medicines.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Operations,
                         l["Menu:Operations"],
                         "/definition/operations",
                         requiredPermissionName: HealthCarePermissions.Operations.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Vaccines,
                         l["Menu:Vaccines"],
                         "/definition/vaccines",
                         requiredPermissionName: HealthCarePermissions.Vaccines.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.BloodTransfusions,
                         l["Menu:BloodTransfusions"],
                         "/definition/blood-transfusions",
                         requiredPermissionName: HealthCarePermissions.BloodTransfusions.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Jobs,
                         l["Menu:Jobs"],
                         "/definition/jobs",
                         requiredPermissionName: HealthCarePermissions.Jobs.Menu
                     )
                 )
                 .AddItem(
                     new ApplicationMenuItem(
                         HealthCareMenus.Educations,
                         l["Menu:Educations"],
                         "/definition/educations",
                         requiredPermissionName: HealthCarePermissions.Educations.Menu
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