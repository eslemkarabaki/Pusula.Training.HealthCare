using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Educations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class EducationConfigurations : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Educations", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasMaxLength(EducationConsts.NameMaxLength).IsRequired();
    }
}