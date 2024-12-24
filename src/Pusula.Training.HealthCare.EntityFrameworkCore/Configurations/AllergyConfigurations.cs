using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Allergies;
using Pusula.Training.HealthCare.PatientHistoryAllergies;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class AllergyConfigurations : IEntityTypeConfiguration<Allergy>
{
    public void Configure(EntityTypeBuilder<Allergy> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Allergies", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasColumnName(nameof(Allergy.Name)).HasMaxLength(AllergyConsts.NameMaxLength)
         .IsRequired();

    }
}