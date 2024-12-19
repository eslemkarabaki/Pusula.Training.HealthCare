using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.ProtocolTypeActions;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class ProtocolTypeActionConfigurations : IEntityTypeConfiguration<ProtocolTypeAction>
{
    public void Configure(EntityTypeBuilder<ProtocolTypeAction> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "ProtocolTypeActions", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b
            .Property(e => e.Name)
            .HasColumnName(nameof(ProtocolTypeAction.Name))
            .HasMaxLength(ProtocolTypeActionConsts.NameMaxLength)
            .IsRequired();

        b
            .HasOne<ProtocolTypes.ProtocolType>()
            .WithMany()
            .HasForeignKey(x => x.ProtocolTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}