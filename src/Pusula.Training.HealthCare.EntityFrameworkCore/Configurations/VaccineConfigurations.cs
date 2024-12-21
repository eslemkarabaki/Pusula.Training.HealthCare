using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Vaccines;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class VaccineConfigurations : IEntityTypeConfiguration<Vaccine>
{
    public void Configure(EntityTypeBuilder<Vaccine> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Vaccines", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasMaxLength(VaccineConsts.NameMaxLength).IsRequired();

        b.HasMany(e => e.Vaccines)
         .WithOne()
         .HasForeignKey(e => e.VaccineId)
         .IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
    }
}