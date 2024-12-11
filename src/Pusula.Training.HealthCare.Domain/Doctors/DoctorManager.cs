using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pusula.Training.HealthCare.GlobalExceptions;
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
        int workingHours,
        Guid titleId,
        Guid departmentId,
        Guid hospitalId,
        string userName,
        string email,
        string password
    )
    {
        var user = await CreateDoctorUserAsync(firstName, lastName, userName, email, password);

        var doctor = new Doctor(
            GuidGenerator.Create(),
            firstName, lastName, workingHours, titleId, departmentId, hospitalId, user!.Id
        );
        return await doctorRepository.InsertAsync(doctor);
    }

    public virtual async Task<Doctor> UpdateAsync(
        Guid id,
        string firstName,
        string lastName,
        int workingHours,
        Guid titleId,
        Guid departmentId,
        [CanBeNull] string? concurrencyStamp = null
    )
    {
        var doctor = await doctorRepository.GetAsync(id);

        doctor.SetName(firstName, lastName);
        doctor.SetWorkingHours(workingHours);
        doctor.SetTitleId(titleId);
        doctor.SetDepartmentId(departmentId);

        doctor.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await doctorRepository.UpdateAsync(doctor);
    }

    protected virtual async Task<IdentityUser> CreateDoctorUserAsync(
        string firstName,
        string lastName,
        string userName,
        string email,
        string password
    )
    {
        await userManager.CreateAsync(
            new IdentityUser(GuidGenerator.Create(), userName, email)
            {
                Name = firstName,
                Surname = lastName
            }, password
        );
        var user = await userManager.FindByNameAsync(userName);
        await userManager.AddToRoleAsync(user!, "doctor");
        return user!;
    }
    
    protected virtual async Task<IdentityUser> UpdateDoctorUserAsync(
        Guid userId,
        string firstName,
        string lastName,
        string userName,
        string email,
        string password
    )
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        user.Name = firstName;
        user.Surname = firstName;
        await userManager.SetUserNameAsync(user,userName);
        await userManager.SetEmailAsync(user, email);
        await userManager.ChangePasswordAsync(user, "", password);
            
            
        await userManager.UpdateAsync(user);
        await userManager.AddToRoleAsync(user!, "doctor");
        return user!;
    }
}