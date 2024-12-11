using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.Hospitals;
using Pusula.Training.HealthCare.Departments;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors;

public class Doctor : FullAuditedAggregateRoot<Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName { get; private set; }
    public int WorkingHours { get; private set; }

    public Guid TitleId { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid HospitalId { get; private set; }
    public Guid UserId { get; private set; }

    protected Doctor()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        FullName = string.Empty;
    }

    public Doctor(
        Guid id,
        string firstName,
        string lastName,
        int workingHours,
        Guid titleId,
        Guid departmentId,
        Guid hospitalId
    ) : base(id)
    {
        SetName(firstName, lastName);
        SetWorkingHours(workingHours);
        SetTitleId(titleId);
        SetDepartmentId(departmentId);
        SetHospitalId(hospitalId);
    }
    
    public Doctor(
        Guid id,
        string firstName,
        string lastName,
        int workingHours,
        Guid titleId,
        Guid departmentId,
        Guid hospitalId,
        Guid userId
    ) : this(id, firstName, lastName, workingHours, titleId, departmentId, hospitalId)
    {
        SetUserId(userId);
    }

    private void SetFirstName(string firstName) =>
        FirstName = Check.NotNullOrWhiteSpace(firstName, nameof(firstName), DoctorConsts.FirstNameMaxLength);

    private void SetLastName(string lastName) =>
        LastName = Check.NotNullOrWhiteSpace(lastName, nameof(lastName), DoctorConsts.LastNameMaxLength);

    private void SetFullName(string firstName, string lastName) => FullName = $"{firstName} {lastName}";

    public void SetName(string firstName, string lastName)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetFullName(firstName, lastName);
    }

    public void SetWorkingHours(int workingHours)
    {
        Check.NotDefaultOrNull<int>(workingHours, nameof(workingHours));
        WorkingHours = Check.Range(workingHours, nameof(workingHours), DoctorConsts.WorkingHoursMin, DoctorConsts.WorkingHoursMax);
    }

    public void SetTitleId(Guid titleId) => TitleId = Check.NotDefaultOrNull<Guid>(titleId, nameof(titleId));

    public void SetDepartmentId(Guid departmentId) =>
        DepartmentId = Check.NotDefaultOrNull<Guid>(departmentId, nameof(departmentId));

    public void SetHospitalId(Guid hospitalId) =>
        HospitalId = Check.NotDefaultOrNull<Guid>(hospitalId, nameof(hospitalId));

    public void SetUserId(Guid userId) => UserId = Check.NotDefaultOrNull<Guid>(userId, nameof(userId));
}