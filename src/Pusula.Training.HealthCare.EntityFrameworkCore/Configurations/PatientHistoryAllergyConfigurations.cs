using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Allergies;
using Pusula.Training.HealthCare.PatientHistoryAllergies;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientHistoryAllergyConfigurations : IEntityTypeConfiguration<PatientHistoryAllergy>
{
    public void Configure(EntityTypeBuilder<PatientHistoryAllergy> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "PatientHistoryAllergies", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Explanation).HasMaxLength(AllergyConsts.ExplanationMaxLength);

        b.HasKey(
            e => new
            {
                e.AllergyId,
                e.PatientHistoryId
            }
        );
    }
}