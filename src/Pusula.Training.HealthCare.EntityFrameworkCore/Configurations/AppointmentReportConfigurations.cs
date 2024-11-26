using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.AppointmentReports;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Hospitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations
{
    public class AppointmentReportConfigurations : IEntityTypeConfiguration<AppointmentReport>
    {
        public void Configure(EntityTypeBuilder<AppointmentReport> b)
        {
            b.ToTable(HealthCareConsts.DbTablePrefix + "AppointmentReports", HealthCareConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.ReportDate).HasColumnName(nameof(AppointmentReport.ReportDate)).IsRequired();
            b.Property(x => x.PriorityNotes).HasColumnName(nameof(AppointmentReport.PriorityNotes)).HasMaxLength(AppointmentReportConsts.PriorityNotesMaxLength);
            b.Property(x => x.DoctorNotes).HasColumnName(nameof(AppointmentReport.DoctorNotes)).HasMaxLength(AppointmentReportConsts.DoctorNotesMaxLength);

            b.HasOne<Appointment>().WithMany().IsRequired().HasForeignKey(x => x.AppointmentId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
