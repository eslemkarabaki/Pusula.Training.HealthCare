using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.AppDefaults;

public class AppDefault : FullAuditedEntity<Guid>
{
    public Guid? CurrentCountryId { get; set; }

    protected AppDefault() { }
    public AppDefault(Guid id) : base(id) { }

    public void SetCurrentCountryId(Guid currentCountryId) =>
        CurrentCountryId = Check.NotDefaultOrNull<Guid>(currentCountryId, nameof(currentCountryId));
}