using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.BloodTransfusions;
using Pusula.Training.HealthCare.PatientHistoryBloodTransfusions;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class BloodTransfusionConfigurations : IEntityTypeConfiguration<BloodTransfusion>
{
    public void Configure(EntityTypeBuilder<BloodTransfusion> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "BloodTransfusions", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasColumnName(nameof(BloodTransfusion.Name))
         .HasMaxLength(BloodTransfusionConsts.NameMaxLength).IsRequired();

    }
}