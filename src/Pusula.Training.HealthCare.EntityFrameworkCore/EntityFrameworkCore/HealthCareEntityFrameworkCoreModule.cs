using System;
using Microsoft.Extensions.DependencyInjection;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.AppDefaults;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Notifications;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.PatientNotes;
using Pusula.Training.HealthCare.PatientTypes;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.RadiologyExaminationGroups;
using Pusula.Training.HealthCare.RadiologyExaminationProcedures;
using Pusula.Training.HealthCare.RadiologyExaminations;

namespace Pusula.Training.HealthCare.EntityFrameworkCore;

[DependsOn(
    typeof(HealthCareDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
)]
public class HealthCareEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        HealthCareEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<HealthCareDbContext>(
            options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(true);

            options.AddRepository<Patient, EfCorePatientRepository>();
            options.AddRepository<Protocol, EfCoreProtocolRepository>();
            options.AddRepository<Department, EfCoreDepartmentRepository>();
            options.AddRepository<Appointment, EfCoreAppointmentRepository>();
            options.AddRepository<Hospital, EfCoreHospitalRepository>();
            options.AddRepository<Notification, EfCoreNotificationRepository>();
            options.AddRepository<Hospital, EfCoreHospitalRepository>();
            options.AddRepository<Notification, EfCoreNotificationRepository>();
            options.AddRepository<Country, EfCoreCountryRepository>();
            options.AddRepository<City, EfCoreCityRepository>();
            options.AddRepository<District, EfCoreDistrictRepository>();
            options.AddRepository<Address, EfCoreAddressRepository>();
            options.AddRepository<Doctor, EfCoreDoctorRepository>();
            options.AddRepository<Title, EfCoreTitleRepository>();
            options.AddRepository<RadiologyExaminationGroup, EfCoreRadiologyExaminationGroupRepository>();
            options.AddRepository<RadiologyExamination, EfCoreRadiologyExaminationRepository>();
            options.AddRepository<RadiologyExaminationProcedure, EfCoreRadiologyExaminationProcedureRepository>();
            options.AddRepository<RadiologyExaminationGroup, EfCoreRadiologyExaminationGroupRepository>();
        });
                options.AddRepository<Patient, EfCorePatientRepository>();
                options.AddRepository<Protocol, EfCoreProtocolRepository>();
                options.AddRepository<Department, EfCoreDepartmentRepository>();
                options.AddRepository<Appointment, EfCoreAppointmentRepository>();
                options.AddRepository<Hospital, EfCoreHospitalRepository>();
                options.AddRepository<Notification, EfCoreNotificationRepository>();
                options.AddRepository<Hospital, EfCoreHospitalRepository>();
                options.AddRepository<Notification, EfCoreNotificationRepository>();
                options.AddRepository<Country, EfCoreCountryRepository>();
                options.AddRepository<City, EfCoreCityRepository>();
                options.AddRepository<District, EfCoreDistrictRepository>();
                options.AddRepository<Address, EfCoreAddressRepository>();
                options.AddRepository<Doctor, EfCoreDoctorRepository>();
                options.AddRepository<Title, EfCoreTitleRepository>();
                options.AddRepository<PatientType, EfCorePatientTypeRepository>();
                options.AddRepository<PatientNote, EfCorePatientNoteRepository>();
                options.AddRepository<AppDefault, EfCoreAppDefaultRepository>();
            }
        );

        Configure<AbpDbContextOptions>(
            options =>
            {
                /* The main point to change your DBMS.
                 * See also HealthCareMigrationsDbContextFactory for EF Core tooling. */
                options.UseNpgsql();
            }
        );
    }
}