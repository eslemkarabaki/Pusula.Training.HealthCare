using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Departments;

namespace Pusula.Training.HealthCare.Doctors;

public class Doctor : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public virtual string FirstName { get; set; }

    [NotNull] public virtual string LastName { get; set; }

    [NotNull] public virtual string WorkingHours { get; set; }


    public virtual Guid TitleId { get; set; }
    public virtual Guid DepartmentId { get; set; }
    public virtual Guid HospitalId { get; set; }

    protected Doctor()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        WorkingHours = string.Empty;
    }

    public Doctor(Guid id, string firstName, string lastName, string workingHours, Guid titleId, Guid departmentId,
                  Guid hospitalId)
    {
        Id = id;
        Check.NotNull(firstName, nameof(firstName));
        Check.Length(firstName, nameof(firstName), DoctorConsts.FirstNameMaxLength, 0);
        Check.NotNull(lastName, nameof(lastName));
        Check.Length(lastName, nameof(lastName), DoctorConsts.LastNameMaxLength, 0);
        Check.NotNull(workingHours, nameof(workingHours));
        Check.Length(workingHours, nameof(workingHours), DoctorConsts.WorkingHoursMaxLength, 0);

        FirstName = firstName;
        LastName = lastName;
        WorkingHours = workingHours;
        TitleId = titleId;
        DepartmentId = departmentId;
        HospitalId = hospitalId;
    }
}