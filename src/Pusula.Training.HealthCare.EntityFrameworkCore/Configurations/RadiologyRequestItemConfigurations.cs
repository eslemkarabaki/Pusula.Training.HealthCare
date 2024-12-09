using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadiologyRequests;
using Pusula.Training.HealthCare.RadioloyRequestItems;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;
public class RadiologyRequestItemConfigurations : IEntityTypeConfiguration<RadiologyRequestItem>
{
    public void Configure(EntityTypeBuilder<RadiologyRequestItem> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "RadiologyRequestItems", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(x => x.RequestId)
            .HasColumnName(nameof(RadiologyRequestItem.RequestId))
            .IsRequired();

        b.Property(x => x.ExaminationId)
            .HasColumnName(nameof(RadiologyRequestItem.ExaminationId))
            .IsRequired();

        b.Property(x => x.Result)
            .HasColumnName(nameof(RadiologyRequestItem.Result))
            .IsRequired()
            .HasMaxLength(RadiologyRequestItemConsts.ResultMaxLength);

        b.Property(x => x.ResultDate)
            .HasColumnName(nameof(RadiologyRequestItem.ResultDate))
            .IsRequired();

        b.Property(x => x.State)
            .HasColumnName(nameof(RadiologyRequestItem.State))
            .IsRequired();

        b.HasOne<RadiologyRequest>()
            .WithMany()
            .HasForeignKey(x => x.RequestId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        b.HasOne<RadiologyExamination>()
            .WithMany()
            .HasForeignKey(x => x.ExaminationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}