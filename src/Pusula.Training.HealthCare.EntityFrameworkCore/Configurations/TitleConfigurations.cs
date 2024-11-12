using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class TitleConfigurations:IEntityTypeConfiguration<Title>
{
    public void Configure(EntityTypeBuilder<Title> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Titles", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.Name).HasColumnName(nameof(Title.Name)).IsRequired()
         .HasMaxLength(TitleConsts.NameMaxLength);
    }
}