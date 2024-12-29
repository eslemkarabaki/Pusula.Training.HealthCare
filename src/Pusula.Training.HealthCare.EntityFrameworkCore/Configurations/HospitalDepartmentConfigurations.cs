using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.HospitalDepartments;
using Pusula.Training.HealthCare.Hospitals;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class HospitalDepartmentConfigurations:IEntityTypeConfiguration<HospitalDepartment>
{
    public void Configure(EntityTypeBuilder<HospitalDepartment> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "HospitalDepartments", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();

        b.HasKey(dh => new { dh.HospitalId, dh.DepartmentId });

        b.HasOne<Department>()
         .WithMany(d => d.HospitalDepartments)
         .HasForeignKey(dh => dh.DepartmentId)
         .IsRequired()
         .OnDelete(DeleteBehavior.NoAction);

        b.HasOne<Hospital>()
         .WithMany(h => h.HospitalDepartments)
         .HasForeignKey(dh => dh.HospitalId)
         .IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
        b.HasIndex(dh => new { dh.HospitalId, dh.DepartmentId });
    }
}