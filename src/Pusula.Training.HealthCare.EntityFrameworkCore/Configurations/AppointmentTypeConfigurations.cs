using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.AppointmentTypes;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations
{
    public class AppointmentTypeConfigurations : IEntityTypeConfiguration<AppointmentType>    
    {
        public void Configure(EntityTypeBuilder<AppointmentType> b)
        {
            b.ToTable(HealthCareConsts.DbTablePrefix + "AppointmentTypes", HealthCareConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).HasColumnName(nameof(AppointmentType.Name)).HasMaxLength(AppointmentTypeConsts.NameMaxLength).IsRequired();
            
        }
    }
}
