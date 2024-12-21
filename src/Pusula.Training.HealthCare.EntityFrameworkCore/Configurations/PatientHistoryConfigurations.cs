using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.PatientHistories;
using Pusula.Training.HealthCare.Patients;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientHistoryConfigurations : IEntityTypeConfiguration<PatientHistory>
{
    public void Configure(EntityTypeBuilder<PatientHistory> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "PatientHistories", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasOne<Patient>()
         .WithOne()
         .IsRequired()
         .HasForeignKey<PatientHistory>(e => e.PatientId)
         .OnDelete(DeleteBehavior.NoAction);
        
        b.HasOne(e=>e.Job)
         .WithOne()
         .IsRequired()
         .HasForeignKey<PatientHistory>(e => e.JobId)
         .OnDelete(DeleteBehavior.NoAction);
        
        b.HasOne(e=>e.Education)
         .WithOne()
         .IsRequired()
         .HasForeignKey<PatientHistory>(e => e.EducationId)
         .OnDelete(DeleteBehavior.NoAction);

        b.HasMany(e => e.Medicines)
         .WithOne()
         .HasForeignKey(e => e.MedicineId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(e => e.Allergies)
         .WithOne()
         .HasForeignKey(e => e.AllergyId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(e => e.Vaccines)
         .WithOne()
         .HasForeignKey(e => e.VaccineId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(e => e.BloodTransfusions)
         .WithOne()
         .HasForeignKey(e => e.BloodTransfusionId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(e => e.Operations)
         .WithOne()
         .HasForeignKey(e => e.OperationId)
         .IsRequired()
         .OnDelete(DeleteBehavior.Cascade);
    }
}