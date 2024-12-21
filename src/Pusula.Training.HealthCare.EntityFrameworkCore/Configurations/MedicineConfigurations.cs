using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Medicines;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class MedicineConfigurations : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Medicines", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasMaxLength(MedicineConsts.NameMaxLength).IsRequired();

        b.HasMany(e => e.Medicines)
         .WithOne()
         .HasForeignKey(e => e.MedicineId)
         .IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
    }
}