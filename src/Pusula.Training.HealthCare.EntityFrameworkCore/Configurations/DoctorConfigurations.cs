using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Titles;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class DoctorConfigurations:IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Doctors", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.FirstName).HasColumnName(nameof(Doctor.FirstName)).IsRequired()
         .HasMaxLength(DoctorConsts.FirstNameMaxLength);
        b.Property(x => x.LastName).HasColumnName(nameof(Doctor.LastName)).IsRequired()
         .HasMaxLength(DoctorConsts.LastNameMaxLength);
        b.Property(x => x.FullName).HasColumnName(nameof(Doctor.FullName));
        b.Property(x => x.WorkingHours).HasColumnName(nameof(Doctor.WorkingHours)).IsRequired();
        b.HasOne<Title>().WithMany().HasForeignKey(x => x.TitleId).IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
        b.HasOne<Hospital>().WithMany().HasForeignKey(x => x.HospitalId).IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
    }
}