using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.RadiologyExaminationProcedures;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations
{
    public class RadiologyExaminationProcedureConfigurations : IEntityTypeConfiguration<RadiologyExaminationProcedure>
    {
        public void Configure(EntityTypeBuilder<RadiologyExaminationProcedure> b)
        {
            b.ToTable(HealthCareConsts.DbTablePrefix + "RadiologyExaminationProcedures", HealthCareConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(e => e.Id);

            b.Property(e => e.Result).HasColumnName(nameof(RadiologyExaminationProcedure.Result)).IsRequired();
            b.Property(e => e.ResultDate).HasColumnName(nameof(RadiologyExaminationProcedure.ResultDate)).IsRequired();
            b.Property(e => e.DoctorId).HasColumnName(nameof(RadiologyExaminationProcedure.DoctorId)).IsRequired();
            b.Property(e => e.ProtocolId).HasColumnName(nameof(RadiologyExaminationProcedure.ProtocolId)).IsRequired();
            b.Property(e => e.RadiologyExaminationId).HasColumnName(nameof(RadiologyExaminationProcedure.RadiologyExaminationId)).IsRequired();

            b.HasOne<RadiologyExamination>()
                .WithMany()
                .HasForeignKey(e => e.RadiologyExaminationId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(e => e.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne<Protocol>()
                .WithMany()
                .HasForeignKey(e => e.ProtocolId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}