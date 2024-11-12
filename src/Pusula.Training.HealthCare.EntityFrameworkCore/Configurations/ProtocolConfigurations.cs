using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class ProtocolConfigurations : IEntityTypeConfiguration<Protocol>
{
    public void Configure(EntityTypeBuilder<Protocol> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Protocols", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.Type).HasColumnName(nameof(Protocol.Type)).IsRequired()
         .HasMaxLength(ProtocolConsts.TypeMaxLength);
        b.Property(x => x.StartTime).HasColumnName(nameof(Protocol.StartTime));
        b.Property(x => x.EndTime).HasColumnName(nameof(Protocol.EndTime));
        b.HasOne<Patient>().WithMany().IsRequired().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Department>().WithMany().IsRequired().HasForeignKey(x => x.DepartmentId)
         .OnDelete(DeleteBehavior.NoAction);
    }
}