using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.BloodTransfusions;
using Pusula.Training.HealthCare.PatientHistoryBloodTransfusions;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientHistoryBloodTransfusionConfigurations : IEntityTypeConfiguration<PatientHistoryBloodTransfusion>
{
    public void Configure(EntityTypeBuilder<PatientHistoryBloodTransfusion> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "PatientHistoryBloodTransfusions", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        
        b.Property(e => e.Explanation).HasMaxLength(BloodTransfusionConsts.ExplanationMaxLength);

        b.HasKey(
            e => new
            {
                e.BloodTransfusionId,
                e.PatientHistoryId
            }
        );
    }
}