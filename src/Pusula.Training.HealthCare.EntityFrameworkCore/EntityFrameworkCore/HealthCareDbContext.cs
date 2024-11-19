using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.HospitalDepartments;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Notifications;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.Tests;
using Pusula.Training.HealthCare.TestTypes;
using Pusula.Training.HealthCare.TestProcesses;
using Pusula.Training.HealthCare.WorkLists;
using System.Reflection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

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
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Hospital> Hospitals { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Title> Titles { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<Examination> Examinations { get; set; } = null!;
    public DbSet<TestGroup> TestGroups { get; set; } = null!;
    public DbSet<Test> Tests { get; set; } = null!;
    public DbSet<TestType> TestTypes { get; set; } = null!;
    public DbSet<TestProcess> TestProcesses { get; set; } = null!;
    public DbSet<WorkList> WorkLists { get; set; } = null!;

    #region Entities from the modules

    // Identity
    public DbSet<IdentityUser> Users { get; set; } = null!;
    public DbSet<IdentityRole> Roles { get; set; } = null!;
    public DbSet<IdentityClaimType> ClaimTypes { get; set; } = null!;
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; } = null!;
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; } = null!;
    public DbSet<IdentityLinkUser> LinkUsers { get; set; } = null!;
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; } = null!;
    public DbSet<IdentitySession> Sessions { get; set; } = null!;

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; } = null!;

    #endregion

    public HealthCareDbContext(DbContextOptions<HealthCareDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure ABP Modules
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

        // Configure TestGroups
        builder.Entity<TestGroup>(b =>
        {
            b.ToTable("TestGroups");
            b.ConfigureByConvention();
            b.Property(x => x.Code).IsRequired().HasMaxLength(32);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });

        // Configure Tests
        builder.Entity<Test>(b =>
        {
            b.ToTable("Tests");
            b.ConfigureByConvention();
            b.Property(x => x.Code).IsRequired().HasMaxLength(32);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.HasOne(x => x.TestGroup).WithMany().HasForeignKey(x => x.GroupId).IsRequired();
        });

        // Configure TestTypes
        builder.Entity<TestType>(b =>
        {
            b.ToTable("TestTypes");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });

        // Configure TestProcesses
        builder.Entity<TestProcess>(b =>
        {
            b.ToTable("TestProcesses");
            b.ConfigureByConvention();
            b.Property(x => x.Result).HasMaxLength(256);
            b.HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientId).IsRequired();
            b.HasOne(x => x.Doctor).WithMany().HasForeignKey(x => x.DoctorId).IsRequired();
            b.HasOne(x => x.Test).WithMany().HasForeignKey(x => x.TestId).IsRequired();
        });

        // Configure WorkLists
        builder.Entity<WorkList>(b =>
        {
            b.ToTable("WorkLists");
            b.ConfigureByConvention();
            b.HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientId).IsRequired();
            b.HasOne(x => x.Doctor).WithMany().HasForeignKey(x => x.DoctorId).IsRequired();
            b.HasOne(x => x.Test).WithMany().HasForeignKey(x => x.TestId).IsRequired();
        });
    }
}
