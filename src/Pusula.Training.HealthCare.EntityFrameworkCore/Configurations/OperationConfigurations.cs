using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Medicines;
using Pusula.Training.HealthCare.Operations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class OperationConfigurations : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Operations", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasMaxLength(OperationConsts.NameMaxLength).IsRequired();

        b.HasMany(e => e.Operations)
         .WithOne()
         .HasForeignKey(e => e.OperationId)
         .IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
    }
}