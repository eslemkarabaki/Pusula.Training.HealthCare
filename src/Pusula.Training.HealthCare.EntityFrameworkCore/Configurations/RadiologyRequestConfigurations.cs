using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.RadiologyRequests;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;
public class RadiologyRequestConfigurations : IEntityTypeConfiguration<RadiologyRequest>
{
    public void Configure(EntityTypeBuilder<RadiologyRequest> b)
    { 
        b.ToTable(HealthCareConsts.DbTablePrefix + "RadiologyRequests", HealthCareConsts.DbSchema);
         
        b.ConfigureByConvention();
         
        b.Property(x => x.RequestDate)
            .HasColumnName(nameof(RadiologyRequest.RequestDate))
            .IsRequired();

        b.Property(x => x.ProtocolId)
            .HasColumnName(nameof(RadiologyRequest.ProtocolId))
            .IsRequired();

        b.Property(x => x.DepartmentId)
            .HasColumnName(nameof(RadiologyRequest.DepartmentId))
            .IsRequired();

        b.Property(x => x.DoctorId)
            .HasColumnName(nameof(RadiologyRequest.DoctorId))
            .IsRequired();
         
        b.HasOne<Protocol>() 
            .WithMany()
            .HasForeignKey(x => x.ProtocolId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);  

        b.HasOne<Department>()  
            .WithMany()
            .HasForeignKey(x => x.DepartmentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);  

        b.HasOne<Doctor>()  
            .WithMany()
            .HasForeignKey(x => x.DoctorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction); 
    }
}