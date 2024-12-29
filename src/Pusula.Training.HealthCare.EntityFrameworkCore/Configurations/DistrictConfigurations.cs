using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Districts;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class DistrictConfigurations : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Districts", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.HasIndex(e => e.CityId);

        b
            .Property(e => e.Name)
            .HasColumnName(nameof(District.Name))
            .IsRequired()
            .HasMaxLength(DistrictConsts.NameMaxLength);

        b
            .HasOne(e => e.City)
            .WithMany()
            .IsRequired(false)
            .HasForeignKey(e => e.CityId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}