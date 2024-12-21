using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.BloodTransfusions;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class BloodTransfusionConfigurations : IEntityTypeConfiguration<BloodTransfusion>
{
    public void Configure(EntityTypeBuilder<BloodTransfusion> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "BloodTransfusions", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasMaxLength(BloodTransfusionConsts.NameMaxLength).IsRequired();

        b.HasMany(e => e.BloodTransfusions)
         .WithOne()
         .HasForeignKey(e => e.BloodTransfusionId)
         .IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
    }
}