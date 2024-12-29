using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.ProtocolTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class ProtocolConfigurations : IEntityTypeConfiguration<Protocol>
{
    public void Configure(EntityTypeBuilder<Protocol> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Protocols", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.Property(x => x.ProtocolNo).HasColumnName(nameof(Protocol.ProtocolNo)).ValueGeneratedOnAdd();
        b.Property(x => x.Status).HasColumnName(nameof(Protocol.Status)).IsRequired();
        b
            .Property(x => x.Description)
            .HasColumnName(nameof(Protocol.Description))
            .IsRequired(false)
            .HasMaxLength(ProtocolConsts.DescriptionMaxLength);
        b.Property(x => x.StartTime).HasColumnName(nameof(Protocol.StartTime)).IsRequired();
        b.Property(x => x.EndTime).HasColumnName(nameof(Protocol.EndTime)).IsRequired(false);

        b
            .HasOne(e => e.Patient)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.NoAction);
        b
            .HasOne(e => e.Department)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);
        b
            .HasOne(e => e.Doctor)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x => x.DoctorId)
            .OnDelete(DeleteBehavior.NoAction);
        b
            .HasOne(e => e.ProtocolType)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x => x.ProtocolTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        b
            .HasOne(e => e.ProtocolTypeAction)
            .WithMany()
            .IsRequired()
            .HasForeignKey(x => x.ProtocolTypeActionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}