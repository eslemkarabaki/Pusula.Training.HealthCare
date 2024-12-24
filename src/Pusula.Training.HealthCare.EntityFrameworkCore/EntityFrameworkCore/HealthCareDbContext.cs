using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Notifications;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using System.Reflection;
using Pusula.Training.HealthCare.Allergies;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Pusula.Training.HealthCare.RadiologyExaminationGroups;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadiologyExaminationProcedures;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.Tests;
using Pusula.Training.HealthCare.TestTypes;
using Pusula.Training.HealthCare.TestProcesses;
using Pusula.Training.HealthCare.WorkLists;
using Pusula.Training.HealthCare.AppDefaults;
using Pusula.Training.HealthCare.BloodTransfusions;
using Pusula.Training.HealthCare.PatientNotes;
using Pusula.Training.HealthCare.PatientTypes;
using ProtocolType = Pusula.Training.HealthCare.ProtocolTypes.ProtocolType;
using Pusula.Training.HealthCare.RadiologyRequests;
using Pusula.Training.HealthCare.RadioloyRequestItems;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.Educations;
using Pusula.Training.HealthCare.ProtocolTypeActions;
using Pusula.Training.HealthCare.ExaminationsPhysical;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.Jobs;
using Pusula.Training.HealthCare.Medicines;
using Pusula.Training.HealthCare.Operations;
using Pusula.Training.HealthCare.PatientHistories;
using Pusula.Training.HealthCare.Vaccines;

namespace Pusula.Training.HealthCare.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class HealthCareDbContext :
    AbpDbContext<HealthCareDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Department> Departments { get; set; } = null!;

    public DbSet<Protocol> Protocols { get; set; } = null!;
    public DbSet<ProtocolType> ProtocolTypes { get; set; } = null!;
    public DbSet<ProtocolTypeAction> ProtocolTypeActions { get; set; } = null!;

    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<PatientType> PatientTypes { get; set; } = null!;
    public DbSet<PatientNote> PatientNotes { get; set; } = null!;
    public DbSet<Hospital> Hospitals { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Title> Titles { get; set; } = null!;

    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;

    public DbSet<Appointment> Appointments { get; set; } = null!;

    public DbSet<AppointmentType> AppointmentTypes { get; set; } = null!;

    //public DbSet<HospitalDepartment> HospitalDepartment { get; set; } = null!; 

    public DbSet<Insurance> Insurances { get; set; } = null!;
    public DbSet<AppDefault> AppDefaults { get; set; } = null!;
    public DbSet<TestGroup> TestGroups { get; set; } = null!;
    public DbSet<Test> Tests { get; set; } = null!;
    public DbSet<TestType> TestTypes { get; set; } = null!;
    public DbSet<TestProcess> TestProcesses { get; set; } = null!;
    public DbSet<WorkList> WorkLists { get; set; } = null!;
    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;

#region Examinations

    public DbSet<Examination> Examinations { get; set; } = null!;
    public DbSet<ExaminationPhysical> ExaminationPhysical { get; set; } = null!;
    public DbSet<ExaminationDiagnosis> ExaminationDiagnoses { get; set; } = null!;
    public DbSet<ExaminationAnamnez> ExaminationAnamnez { get; set; } = null!;

#endregion

#region Radiology

    public DbSet<RadiologyExaminationGroup> RadiologyExaminationGroups { get; set; } = null!;
    public DbSet<RadiologyExamination> RadiologyExaminations { get; set; } = null!;
    public DbSet<RadiologyExaminationProcedure> RadiologyExaminationProcedures { get; set; } = null!;
    public DbSet<RadiologyExaminationDocument> RadiologyExaminationDocuments { get; set; } = null!;
    public DbSet<RadiologyRequest> RadiologyRequests { get; set; } = null!;
    public DbSet<RadiologyRequestItem> RadiologyRequestItems { get; set; } = null!;

#endregion

    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<BloodTransfusion> BloodTransfusions { get; set; }
    public DbSet<Allergy> Allergies { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Education> Educations { get; set; }

#region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

#endregion

    public HealthCareDbContext(DbContextOptions<HealthCareDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */
        if (builder.IsHostDatabase())
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}