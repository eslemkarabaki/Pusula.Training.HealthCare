using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Jobs;

public class Job : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    protected Job() => Name = string.Empty;

    public Job(Guid id, string name) : base(id) => SetName(name);

    public void SetName(string name) => Name = Check.NotNullOrWhiteSpace(name, nameof(Name), JobConsts.NameMaxLength);
}