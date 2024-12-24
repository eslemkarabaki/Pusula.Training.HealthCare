using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Operations;
using Pusula.Training.HealthCare.PatientHistoryOperations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class OperationConfigurations : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Operations", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Name).HasColumnName(nameof(Operation.Name)).HasMaxLength(OperationConsts.NameMaxLength)
         .IsRequired();
    }
}