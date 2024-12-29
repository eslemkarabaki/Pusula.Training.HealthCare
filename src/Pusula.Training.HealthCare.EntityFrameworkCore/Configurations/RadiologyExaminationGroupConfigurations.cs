using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.RadiologyExaminationGroups;
using Volo.Abp.EntityFrameworkCore.Modeling;
namespace Pusula.Training.HealthCare.Configurations
{
    public class RadiologyExaminationGroupConfigurations : IEntityTypeConfiguration<RadiologyExaminationGroup>
    {
        public void Configure(EntityTypeBuilder<RadiologyExaminationGroup> b)
        {
            b.ToTable(HealthCareConsts.DbTablePrefix + "RadiologyExaminationGroups", HealthCareConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(e => e.Id);

            b.Property(e => e.Name).HasColumnName(nameof(RadiologyExaminationGroup.Name)).IsRequired();
            b.HasIndex(e => e.Name).IsUnique();
            b.Property(e => e.Description).HasColumnName(nameof(RadiologyExaminationGroup.Description));

        }
    }
}
