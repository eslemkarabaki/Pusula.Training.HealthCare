using JetBrains.Annotations;
using Pusula.Training.HealthCare.Hospitals;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Departments;

public class DepartmentManager(IDepartmentRepository departmentRepository, IHospitalRepository hospitalRepository) : DomainService
{
    public virtual async Task<Department> CreateAsync(
    string name,
    string description,
    int duration 
    )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), DepartmentConsts.NameMaxLength);
        Check.Length(description, nameof(description), DepartmentConsts.DescriptionMaxLength);
        Check.Range(duration, nameof(duration), 1, DepartmentConsts.DurationMaxValue);


        var department = new Department(
         GuidGenerator.Create(),
         name,
         description,
         duration
         );

        return await departmentRepository.InsertAsync(department);
    }

    public virtual async Task<Department> UpdateAsync(
        Guid id,
        string name,
        [CanBeNull] string? description = null,
        [CanBeNull] int? duration = null, 
        [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), DepartmentConsts.NameMaxLength);

        var department = await departmentRepository.FindAsync(id);

        department!.Name = name;

        department.Description = description ?? department.Description;
        Check.Length(department.Description, nameof(description), DepartmentConsts.DescriptionMaxLength);

        department.Duration = duration ?? department.Duration;
        Check.Range(department.Duration, nameof(duration), 1, DepartmentConsts.DurationMaxValue);
         
        return await departmentRepository.UpdateAsync(department);
    } 
     
}