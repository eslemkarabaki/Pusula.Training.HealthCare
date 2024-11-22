using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Patients;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Appointments", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.StartTime).HasColumnName(nameof(Appointment.StartTime)).IsRequired();
        b.Property(x => x.EndTime).HasColumnName(nameof(Appointment.EndTime)).IsRequired();
        b.Property(x => x.Status).HasColumnName(nameof(Appointment.Status)).IsRequired().HasMaxLength(AppointmentConsts.NoteMaxLength);
        b.Property(x => x.Note).HasColumnName(nameof(Appointment.Note)).HasMaxLength(AppointmentConsts.NoteMaxLength);

        b.HasOne<AppointmentType>().WithMany().IsRequired().HasForeignKey(x => x.AppointmentTypeId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Department>().WithMany().IsRequired().HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Doctor>().WithMany().IsRequired().HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Patient>().WithMany().IsRequired().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
    }
}