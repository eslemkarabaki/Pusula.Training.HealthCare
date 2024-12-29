using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Patients;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class AddressConfigurations : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Addresses", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.HasIndex(e => e.PatientId);

        b
            .Property(e => e.AddressTitle)
            .HasColumnName(nameof(Address.AddressTitle))
            .IsRequired()
            .HasMaxLength(AddressConsts.TitleMaxLength);
        b
            .Property(e => e.AddressLine)
            .HasColumnName(nameof(Address.AddressLine))
            .IsRequired()
            .HasMaxLength(AddressConsts.AddressMaxLength);

        b
            .HasOne<Patient>()
            .WithMany(e => e.Addresses)
            .IsRequired()
            .HasForeignKey(e => e.PatientId)
            .OnDelete(DeleteBehavior.NoAction);

        b
            .HasOne(e => e.District)
            .WithMany()
            .IsRequired(false)
            .HasForeignKey(e => e.DistrictId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}