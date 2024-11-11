using JetBrains.Annotations;
using Pusula.Training.HealthCare.HospitalDepartments;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Departments;

public class Department : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Name { get; set; }

    [CanBeNull]
    public virtual string Description { get; set; }

    [NotNull]
    public virtual int Duration { get; set; }
    public ICollection<HospitalDepartment> HospitalDepartments { get; set; }


    protected Department()
    {
        Name = string.Empty;
        Description = string.Empty;
        Duration = 15; 
        HospitalDepartments = new List<HospitalDepartment>();
    }

    public Department(Guid id, string name, string description, int duration)
    {
        Id = id;
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), DepartmentConsts.NameMaxLength, 0);
        Name = name;
        Check.Length(description, nameof(description), DepartmentConsts.DescriptionMaxLength, 0);
        Description = description;
        Check.NotNull(duration, nameof(duration));
        Check.Positive(duration, nameof(duration));
        Check.Range(duration, nameof(duration), 0, DepartmentConsts.DurationMaxValue);
        Duration = duration;
        HospitalDepartments = new List<HospitalDepartment>();
    }

}