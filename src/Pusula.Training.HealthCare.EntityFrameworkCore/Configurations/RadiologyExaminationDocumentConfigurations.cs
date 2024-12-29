using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.RadiologyExaminationDocuments;
using Pusula.Training.HealthCare.RadiologyExaminationProcedures;
using Pusula.Training.HealthCare.RadioloyRequestItems;
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
             
            b.Property(e => e.Path).HasColumnName(nameof(RadiologyExaminationDocument.Path)).IsRequired();
            b.Property(e => e.UploadDate).HasColumnName(nameof(RadiologyExaminationDocument.UploadDate)).IsRequired();
            b.Property(e => e.ItemId).HasColumnName(nameof(RadiologyExaminationDocument.ItemId)).IsRequired();

            b.HasOne<RadiologyRequestItem>()
           .WithMany()
           .HasForeignKey(e => e.ItemId)
           .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
