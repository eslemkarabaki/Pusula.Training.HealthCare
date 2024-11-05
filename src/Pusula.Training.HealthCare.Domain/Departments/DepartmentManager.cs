using JetBrains.Annotations;
using Pusula.Training.HealthCare.Hospitals;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Departments;

public class DepartmentManager(IDepartmentRepository departmentRepository, IHospitalRepository hospitalRepository) : DomainService
{
    public virtual async Task<Department> CreateAsync(
    string name,
    string description,
    int duration,
    [CanBeNull] string[] departmentNames
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

        await SetHospitalsAsync(department, departmentNames);

        return await departmentRepository.InsertAsync(department);
    }

    public virtual async Task<Department> UpdateAsync(
        Guid id,
        string name,
        [CanBeNull] string? description = null,
        [CanBeNull] int? duration = null,
        [CanBeNull] string[]? departmentNames = null,
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

        await SetHospitalsAsync(department, departmentNames);

        return await departmentRepository.UpdateAsync(department);
    }

    private async Task SetHospitalsAsync(Department department, [CanBeNull] string[] hospitalNames)
    {
        if (hospitalNames == null || !hospitalNames.Any())
        {
            department.RemoveAllHospitals();
            return;
        }

        var query = (await hospitalRepository.GetQueryableAsync())
            .Where(h =>hospitalNames.Contains(h.Name))
            .Select(d => d.Id)
            .Distinct(); ;

        var hospitalsIds = await AsyncExecuter.ToListAsync(query);

        if (!hospitalsIds.Any())
            return;

        department.RemoveAllHospitalsExceptGivenIds(hospitalsIds);

        foreach (var hospitalId in hospitalsIds)
        {
            department.AddHospital(hospitalId);
        }


    }
}