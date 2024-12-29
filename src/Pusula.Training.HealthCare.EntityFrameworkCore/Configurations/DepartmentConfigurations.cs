using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.HospitalDepartments;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Departments", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.Name).HasColumnName(nameof(Department.Name)).IsRequired()
         .HasMaxLength(DepartmentConsts.NameMaxLength);
        b.Property(x => x.Description).HasColumnName(nameof(Department.Description))
         .HasMaxLength(DepartmentConsts.DescriptionMaxLength);
        b.Property(x => x.Duration).HasColumnName(nameof(Department.Duration)).IsRequired();
        b.HasMany<HospitalDepartment>().WithOne().HasForeignKey(x => x.DepartmentId).IsRequired()
         .OnDelete(DeleteBehavior.NoAction);
    }
}