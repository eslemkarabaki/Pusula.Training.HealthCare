using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.AppDefaults;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class AppDefaultConfigurations : IEntityTypeConfiguration<AppDefault>
{
    public void Configure(EntityTypeBuilder<AppDefault> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "AppDefaults", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.CurrentCountryId).HasColumnName(nameof(AppDefault.CurrentCountryId)).IsRequired(false);
    }
}