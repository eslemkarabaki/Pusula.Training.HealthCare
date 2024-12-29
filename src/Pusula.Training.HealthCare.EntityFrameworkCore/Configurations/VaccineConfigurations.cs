using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Vaccines;
using Pusula.Training.HealthCare.PatientHistoryVaccines;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class VaccineConfigurations : IEntityTypeConfiguration<Vaccine>
{
    public void Configure(EntityTypeBuilder<Vaccine> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Vaccines", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasColumnName(nameof(Vaccine.Name)).HasMaxLength(VaccineConsts.NameMaxLength)
         .IsRequired();
    }
}