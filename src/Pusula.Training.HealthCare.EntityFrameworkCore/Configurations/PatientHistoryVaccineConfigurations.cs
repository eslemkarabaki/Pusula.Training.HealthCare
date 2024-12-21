using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.PatientHistoryVaccines;
using Pusula.Training.HealthCare.Vaccines;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientHistoryVaccineConfigurations : IEntityTypeConfiguration<PatientHistoryVaccine>
{
    public void Configure(EntityTypeBuilder<PatientHistoryVaccine> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "PatientHistoryVaccines", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        
        b.Property(e => e.Explanation).HasMaxLength(VaccineConsts.ExplanationMaxLength);

        b.HasKey(
            e => new
            {
                e.VaccineId,
                e.PatientHistoryId
            }
        );
    }
}