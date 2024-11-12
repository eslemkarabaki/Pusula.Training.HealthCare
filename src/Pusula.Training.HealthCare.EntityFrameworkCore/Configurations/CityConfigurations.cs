using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class CityConfigurations : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Cities", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.HasIndex(e => e.CountryId);

        b.Property(e => e.Name).HasColumnName(nameof(City.Name)).IsRequired()
         .HasMaxLength(CityConsts.NameMaxLength);

        b.HasOne<Country>().WithMany().IsRequired().HasForeignKey(e => e.CountryId)
         .OnDelete(DeleteBehavior.NoAction);
    }
}