using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments;
using Pusula.Training.HealthCare.RadiologyExaminationProcedures;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations
{
    public class RadiologyExaminationDocumentConfigurations : IEntityTypeConfiguration<RadiologyExaminationDocument>
    {
        public void Configure(EntityTypeBuilder<RadiologyExaminationDocument> b)
        {
            b.ToTable(HealthCareConsts.DbTablePrefix + "RadiologyExaminationDocuments", HealthCareConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(e => e.Id);

            b.Property(e => e.DocumentName).HasColumnName(nameof(RadiologyExaminationDocument.DocumentName)).IsRequired();
            b.Property(e => e.DocumentPath).HasColumnName(nameof(RadiologyExaminationDocument.DocumentPath)).IsRequired();
            b.Property(e => e.UploadDate).HasColumnName(nameof(RadiologyExaminationDocument.UploadDate)).IsRequired();
            b.Property(e => e.RadiologyExaminationProcedureId).HasColumnName(nameof(RadiologyExaminationDocument.RadiologyExaminationProcedureId)).IsRequired();

            b.HasOne<RadiologyExaminationProcedure>()
           .WithMany()
           .HasForeignKey(e => e.RadiologyExaminationProcedureId)
           .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
