using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.PatientTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientTypeConfigurations : IEntityTypeConfiguration<PatientType>
{
    public void Configure(EntityTypeBuilder<PatientType> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "PatientTypes", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(e => e.Name).HasColumnName(nameof(PatientType.Name)).IsRequired()
         .HasMaxLength(PatientTypeContst.NameMaxLength);
    }
}