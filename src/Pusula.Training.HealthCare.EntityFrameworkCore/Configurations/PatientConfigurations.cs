using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.PatientHistories;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.PatientTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientConfigurations : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Patients", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.HasIndex(
            e => new
            {
                e.No,
                e.FirstName,
                e.LastName,
                e.IdentityNumber,
                e.PassportNumber
            }
        );

        b.Property(x => x.No).HasColumnName(nameof(Patient.No)).ValueGeneratedOnAdd();

        b.Property(x => x.FirstName)
         .HasColumnName(nameof(Patient.FirstName))
         .IsRequired()
         .HasMaxLength(PatientConsts.FirstNameMaxLength);

        b.Property(x => x.LastName)
         .HasColumnName(nameof(Patient.LastName))
         .IsRequired()
         .HasMaxLength(PatientConsts.LastNameMaxLength);

        b.Property(e => e.FullName).HasColumnName(nameof(Patient.FullName));

        b.Property(x => x.BirthDate).HasColumnName(nameof(Patient.BirthDate));

        b.Property(x => x.IdentityNumber)
         .HasColumnName(nameof(Patient.IdentityNumber))
         .IsRequired(false)
         .HasMaxLength(PatientConsts.IdentityNumberMaxLength);

        b.Property(x => x.PassportNumber)
         .HasColumnName(nameof(Patient.PassportNumber))
         .IsRequired(false)
         .HasMaxLength(PatientConsts.PassportNumberMaxLength);

        b.Property(x => x.EmailAddress)
         .HasColumnName(nameof(Patient.EmailAddress))
         .IsRequired()
         .HasMaxLength(PatientConsts.EmailAddressMaxLength);

        b.Property(x => x.MobilePhoneNumber)
         .HasColumnName(nameof(Patient.MobilePhoneNumber))
         .IsRequired()
         .HasMaxLength(PatientConsts.PhoneNumberMaxLength);

        b.Property(x => x.HomePhoneNumber)
         .HasColumnName(nameof(Patient.HomePhoneNumber))
         .IsRequired(false)
         .HasMaxLength(PatientConsts.PhoneNumberMaxLength);

        b.Property(x => x.Gender).HasColumnName(nameof(Patient.Gender)).IsRequired();
        b.Property(x => x.BloodType).HasColumnName(nameof(Patient.BloodType)).IsRequired();
        b.Property(x => x.MaritalStatus).HasColumnName(nameof(Patient.MaritalStatus)).IsRequired();

        b.HasOne(e => e.Country)
         .WithMany()
         .IsRequired()
         .HasForeignKey(e => e.CountryId)
         .OnDelete(DeleteBehavior.NoAction);

        b.HasOne(e => e.PatientType)
         .WithMany()
         .IsRequired()
         .HasForeignKey(e => e.PatientTypeId)
         .OnDelete(DeleteBehavior.NoAction);
    }
}