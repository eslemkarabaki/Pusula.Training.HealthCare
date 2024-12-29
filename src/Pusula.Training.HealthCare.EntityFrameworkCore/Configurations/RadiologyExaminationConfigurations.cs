using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.RadiologyExaminationGroups;
using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations
{
    public class RadiologyExaminationConfigurations : IEntityTypeConfiguration<RadiologyExamination>
    {
        public void Configure(EntityTypeBuilder<RadiologyExamination> b)
        {
            b.ToTable(HealthCareConsts.DbTablePrefix + "RadiologyExaminations", HealthCareConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasKey(x => x.Id);


            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(RadiologyExaminationConsts.NameMaxLength);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(x => x.ExaminationCode)
                .IsRequired()
                .HasMaxLength(RadiologyExaminationConsts.MaxCodeLength);
            b.Property(re => re.GroupId)
                   .IsRequired();

            b.HasOne<RadiologyExaminationGroup>()
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.GroupId);
        }
    }
}
