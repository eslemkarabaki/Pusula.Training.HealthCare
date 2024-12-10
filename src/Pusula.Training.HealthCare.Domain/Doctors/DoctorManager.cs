using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.Doctors;

public class DoctorManager(IDoctorRepository doctorRepository, UserManager<IdentityUser> userManager) : DomainService
{
    public virtual async Task<Doctor> CreateAsync(
        string firstName,
        string lastName,
        string workingHours,
        Guid titleId,
        Guid departmentId,
        Guid hospitalId
    )
    {
        Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
        Check.Length(firstName, nameof(firstName), DoctorConsts.FirstNameMaxLength);
        Check.NotNullOrWhiteSpace(lastName, nameof(lastName));
        Check.Length(lastName, nameof(lastName), DoctorConsts.LastNameMaxLength);
        Check.NotNullOrWhiteSpace(workingHours, nameof(workingHours));
        Check.Length(workingHours, nameof(workingHours), DoctorConsts.WorkingHoursMaxLength);
        Check.NotNull(titleId, nameof(titleId));
        Check.NotNull(departmentId, nameof(departmentId));
        Check.NotNull(hospitalId, nameof(hospitalId));

        var doctor = new Doctor(
            GuidGenerator.Create(),
            firstName, lastName, workingHours, titleId, departmentId, hospitalId
        );
        return await doctorRepository.InsertAsync(doctor);
    }

    public virtual async Task<Doctor> UpdateAsync(
        Guid id,
        string firstName,
        string lastName,
        string workingHours,
        Guid titleId,
        Guid departmentId,
        Guid hospitalId,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        Check.NotNullOrWhiteSpace(firstName, nameof(firstName));
        Check.Length(firstName, nameof(firstName), DoctorConsts.FirstNameMaxLength);
        Check.NotNullOrWhiteSpace(lastName, nameof(lastName));
        Check.Length(lastName, nameof(lastName), DoctorConsts.LastNameMaxLength);
        Check.NotNullOrWhiteSpace(workingHours, nameof(workingHours));
        Check.Length(workingHours, nameof(workingHours), DoctorConsts.WorkingHoursMaxLength);
        Check.NotNull(titleId, nameof(titleId));
        Check.NotNull(departmentId, nameof(departmentId));
        Check.NotNull(hospitalId, nameof(hospitalId));

        var doctor = await doctorRepository.GetAsync(id);

        doctor.FirstName = firstName;
        doctor.LastName = lastName;
        doctor.WorkingHours = workingHours;
        doctor.TitleId = titleId;
        doctor.DepartmentId = departmentId;
        doctor.HospitalId = hospitalId;

        doctor.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await doctorRepository.UpdateAsync(doctor);
    }
}