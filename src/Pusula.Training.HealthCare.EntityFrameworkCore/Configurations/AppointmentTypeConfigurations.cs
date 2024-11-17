using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.AppointmentReports;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations
{
    public class AppointmentTypeConfigurations : IEntityTypeConfiguration<AppointmentType>    
    {
        public void Configure(EntityTypeBuilder<AppointmentType> b)
        {
            b.ToTable(HealthCareConsts.DbTablePrefix + "AppointmentTypes", HealthCareConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).HasColumnName(nameof(AppointmentType.Name)).HasMaxLength(AppointmentTypeConsts.NameMaxLength);
            
        }
    }
}
