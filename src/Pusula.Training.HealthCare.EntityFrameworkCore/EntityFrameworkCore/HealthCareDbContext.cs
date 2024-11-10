using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.HospitalDepartments;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Notifications;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
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

namespace Pusula.Training.HealthCare.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class HealthCareDbContext :
        AbpDbContext<HealthCareDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
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
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }
        public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
        public DbSet<IdentitySession> Sessions { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
        public DbSet<Doctor> AppDoctors { get; set; }

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

            if (builder.IsHostDatabase())
            {
                #region Doctor Configuration
                builder.Entity<Doctor>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Doctors", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.FirstName).HasColumnName(nameof(Doctor.FirstName)).IsRequired()
                        .HasMaxLength(DoctorConsts.FirstNameMaxLength);
                    b.Property(x => x.LastName).HasColumnName(nameof(Doctor.LastName)).IsRequired()
                        .HasMaxLength(DoctorConsts.LastNameMaxLength);
                    b.Property(x => x.WorkingHours).HasColumnName(nameof(Doctor.WorkingHours)).IsRequired();

                    b.HasOne<Title>().WithMany().HasForeignKey(x => x.TitleId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                    b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                    b.HasOne<Hospital>().WithMany().HasForeignKey(x => x.HospitalId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                });
                #endregion

                #region Title Configuration
                builder.Entity<Title>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Titles", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.Name).HasColumnName(nameof(Title.Name)).IsRequired()
                        .HasMaxLength(TitleConsts.NameMaxLength);
                });
                #endregion

                #region Department Configuration
                builder.Entity<Department>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Departments", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.Name).HasColumnName(nameof(Department.Name)).IsRequired()
                        .HasMaxLength(DepartmentConsts.NameMaxLength);
                });
                #endregion

                #region Hospital Configuration
                builder.Entity<Hospital>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Hospitals", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.Name).HasColumnName(nameof(Hospital.Name)).IsRequired()
                        .HasMaxLength(HospitalConsts.NameMaxLength);
                });
                #endregion

                #region Patient Configuration
                builder.Entity<Patient>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Patients", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.FirstName).HasColumnName(nameof(Patient.FirstName)).IsRequired()
                        .HasMaxLength(PatientConsts.FirstNameMaxLength);
                    b.Property(x => x.LastName).HasColumnName(nameof(Patient.LastName)).IsRequired()
                        .HasMaxLength(PatientConsts.LastNameMaxLength);
                });
                #endregion

                #region Country Configuration
                builder.Entity<Country>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Countries", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.Name).HasColumnName(nameof(Country.Name)).IsRequired()
                        .HasMaxLength(CountryConsts.NameMaxLength);
                });
                #endregion

                #region City Configuration
                builder.Entity<City>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Cities", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.Name).HasColumnName(nameof(City.Name)).IsRequired()
                        .HasMaxLength(CityConsts.NameMaxLength);
                });
                #endregion

                #region District Configuration
                builder.Entity<District>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Districts", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.Name).HasColumnName(nameof(District.Name)).IsRequired()
                        .HasMaxLength(DistrictConsts.NameMaxLength);
                });
                #endregion

                #region Address
                builder.Entity<Address>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Addresses", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention(); 

                    b.Property(x => x.AddressLine).HasColumnName(nameof(Address.AddressLine)).IsRequired()
                        .HasMaxLength(500);  // Burada address line için uygun maksimum uzunluk belirlenmiş.

                    // Foreign key relationships
                    b.HasOne<Patient>().WithMany().HasForeignKey(x => x.PatientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                    b.HasOne<District>().WithMany().HasForeignKey(x => x.DistrictId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                });
                #endregion

                #region AppDoctor Configuration
                builder.Entity<Doctor>(b =>
                {
                    b.ToTable(HealthCareConsts.DbTablePrefix + "Doctor", HealthCareConsts.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.FirstName).HasColumnName(nameof(Doctor.FirstName)).IsRequired()
                        .HasMaxLength(DoctorConsts.FirstNameMaxLength);
                    b.Property(x => x.LastName).HasColumnName(nameof(Doctor.LastName)).IsRequired()
                        .HasMaxLength(DoctorConsts.LastNameMaxLength);
                    b.Property(x => x.WorkingHours).HasColumnName(nameof(Doctor.WorkingHours)).IsRequired();

                    b.HasOne<Title>().WithMany().HasForeignKey(x => x.TitleId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                    b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                    b.HasOne<Hospital>().WithMany().HasForeignKey(x => x.HospitalId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                });
                #endregion
            }
        }
    }
}
