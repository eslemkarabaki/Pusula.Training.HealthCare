using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Examinations;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Configurations
{
    public class ExaminationConfigurations : IEntityTypeConfiguration<Examination>
    {
        public void Configure(EntityTypeBuilder<Examination> b)
        {


            //b.ToTable(HealthCareConsts.DbTablePrefix + "Examination", HealthCareConsts.DbSchema);
            //b.Property(x => x.IdentityNumber).HasColumnName(nameof(Examination.IdentityNumber)).IsRequired()
            //    .HasMaxLength(ExaminationConsts.IdentityNumberMaxLength);
            //b.Property(x => x.VisitDate).HasColumnName(nameof(Examination.VisitDate));
            //b.Property(x => x.Notes).HasColumnName(nameof(Examination.Notes)).IsRequired()
            //    .HasMaxLength(ExaminationConsts.NotesNumberMaxLength);
            //b.Property(x => x.ChronicDiseases).HasColumnName(nameof(Examination.ChronicDiseases)).IsRequired()
            //    .HasMaxLength(ExaminationConsts.ChronicDiseasesNumberMaxLength);
            //b.Property(x => x.Allergies).HasColumnName(nameof(Examination.Allergies)).IsRequired()
            //    .HasMaxLength(ExaminationConsts.AllergiesNumberMaxLength);
            //b.Property(x => x.Medications).HasColumnName(nameof(Examination.Medications)).IsRequired(false)
            //    .HasMaxLength(ExaminationConsts.MedicationsNumberMaxLength);
            //b.Property(x => x.Diagnosis).HasColumnName(nameof(Examination.Diagnosis)).IsRequired(false)
            //    .HasMaxLength(ExaminationConsts.DiagnosisNumberMaxLength);
            //b.Property(x => x.Prescription).HasColumnName(nameof(Examination.Prescription)).IsRequired(false)
            //    .HasMaxLength(ExaminationConsts.PrescriptionNumberMaxLength);
            //b.Property(x => x.ImagingResults).HasColumnName(nameof(Examination.ImagingResults)).IsRequired(false)
            //    .HasMaxLength(ExaminationConsts.ImagingResultsNumberMaxLength);

            //b.HasOne<Patient>().WithMany().IsRequired().HasForeignKey(e => e.PatientId)
            //    .OnDelete(DeleteBehavior.NoAction);
            //b.HasOne<Doctor>().WithMany().IsRequired().HasForeignKey(e => e.DoctorId)
            //    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

