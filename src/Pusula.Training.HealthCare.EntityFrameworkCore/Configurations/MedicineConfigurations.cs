using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Medicines;
using Pusula.Training.HealthCare.PatientHistoryMedicines;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class MedicineConfigurations : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Medicines", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasColumnName(nameof(Medicine.Name)).HasMaxLength(MedicineConsts.NameMaxLength)
         .IsRequired();
    }
}