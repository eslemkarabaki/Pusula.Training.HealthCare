using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.PatientNotes;
using Pusula.Training.HealthCare.Patients;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class PatientNoteConfigurations : IEntityTypeConfiguration<PatientNote>
{
    public void Configure(EntityTypeBuilder<PatientNote> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "PatientNotes", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(e => e.Note).HasColumnName(nameof(PatientNote.Note)).IsRequired()
         .HasMaxLength(PatientNoteConsts.NoteMaxLength);

        b.HasOne<Patient>().WithMany().IsRequired().HasForeignKey(e => e.PatientId).OnDelete(DeleteBehavior.NoAction);
    }
}