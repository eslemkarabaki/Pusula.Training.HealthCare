using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Educations;

public class Education : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    protected Education() => Name = string.Empty;

    public Education(Guid id, string name) : base(id) => SetName(name);

    public void SetName(string name) =>
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), EducationConsts.NameMaxLength);
}