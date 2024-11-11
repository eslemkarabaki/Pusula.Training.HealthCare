using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Appointments;
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
        b.Property(x => x.AppointmentDate).HasColumnName(nameof(Appointment.AppointmentDate)).IsRequired();
        b.Property(x => x.Status).HasColumnName(nameof(Appointment.Status));
        b.Property(x => x.Notes).HasColumnName(nameof(Appointment.Notes));

        b.HasOne<Hospital>().WithMany().IsRequired().HasForeignKey(x => x.HospitalId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Department>().WithMany().IsRequired().HasForeignKey(x => x.DepartmentId)
         .OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Doctor>().WithMany().IsRequired().HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Patient>().WithMany().IsRequired().HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.NoAction);
    }
}