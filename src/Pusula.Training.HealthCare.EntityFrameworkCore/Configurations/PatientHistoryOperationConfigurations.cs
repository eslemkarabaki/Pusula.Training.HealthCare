using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Operations;
using Pusula.Training.HealthCare.PatientHistoryOperations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientHistoryOperationConfigurations : IEntityTypeConfiguration<PatientHistoryOperation>
{
    public void Configure(EntityTypeBuilder<PatientHistoryOperation> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "PatientHistoryOperations", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(e => e.Explanation).HasMaxLength(OperationConsts.ExplanationMaxLength);
        
        b.HasKey(
            e => new
            {
                e.OperationId,
                e.PatientHistoryId
            }
        );
    }
}