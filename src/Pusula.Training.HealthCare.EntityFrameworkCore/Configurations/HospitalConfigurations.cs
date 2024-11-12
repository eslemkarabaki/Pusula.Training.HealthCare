using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.HospitalDepartments;
using Pusula.Training.HealthCare.Hospitals;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class HospitalConfigurations : IEntityTypeConfiguration<Hospital>
{
    public void Configure(EntityTypeBuilder<Hospital> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Hospitals", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.Name).HasColumnName(nameof(Hospital.Name)).IsRequired()
         .HasMaxLength(HospitalConsts.NameMaxLength);
        b.Property(x => x.Address).HasColumnName(nameof(Hospital.Address)).IsRequired()
         .HasMaxLength(HospitalConsts.AddressMaxLength);
        b.HasMany<HospitalDepartment>().WithOne().HasForeignKey(x => x.HospitalId).IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
    }
}