using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.ProtocolTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class ProtocolTypeConfigurations : IEntityTypeConfiguration<ProtocolType>
{
    public void Configure(EntityTypeBuilder<ProtocolType> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "ProtocolTypes", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b
            .Property(x => x.Name)
            .HasColumnName(nameof(ProtocolType.Name))
            .IsRequired()
            .HasMaxLength(ProtocolTypeConsts.NameMaxLength);
    }
}