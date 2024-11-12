using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pusula.Training.HealthCare.Notifications;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Pusula.Training.HealthCare.Configurations;

public class NotificationConfigurations:IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> b)
    {
        b.ToTable(HealthCareConsts.DbTablePrefix + "Notifications", HealthCareConsts.DbSchema);
        b.ConfigureByConvention();
    }
}