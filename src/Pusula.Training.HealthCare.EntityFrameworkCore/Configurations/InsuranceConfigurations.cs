using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pusula.Training.HealthCare.Insurances;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations
{
    public class InsuranceConfigurations : IEntityTypeConfiguration<Insurance>
    {
        public void Configure(EntityTypeBuilder<Insurance> b)
        {
            b.ToTable(HealthCareConsts.DbTablePrefix + "Insurances", HealthCareConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).HasColumnName(nameof(Insurance.Name)).HasMaxLength(InsuranceConsts.NameMaxLength).IsRequired();

        }
    }
}
