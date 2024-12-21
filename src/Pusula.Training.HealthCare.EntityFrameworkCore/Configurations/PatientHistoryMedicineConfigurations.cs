using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Medicines;
using Pusula.Training.HealthCare.PatientHistoryMedicines;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientHistoryMedicineConfigurations : IEntityTypeConfiguration<PatientHistoryMedicine>
{
    public void Configure(EntityTypeBuilder<PatientHistoryMedicine> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "PatientHistoryMedicines", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Explanation).HasMaxLength(MedicineConsts.ExplanationMaxLength);
        
        b.HasKey(
            e => new
            {
                e.MedicineId,
                e.PatientHistoryId
            }
        );
    }
}