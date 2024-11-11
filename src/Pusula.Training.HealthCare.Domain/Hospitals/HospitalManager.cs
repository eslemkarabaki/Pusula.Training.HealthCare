using JetBrains.Annotations;
using Pusula.Training.HealthCare.Departments;
using System;
using System.Linq; 
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Hospitals
{
    public class HospitalManager(IHospitalRepository hospitalRepository, IDepartmentRepository departmentRepository) : DomainService
    {
        public virtual async Task<Hospital> CreateAsync(
                string name,
                string address,
                [CanBeNull] string[] departmentNames)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), HospitalConsts.NameMaxLength);
            Check.NotNullOrWhiteSpace(address, nameof(address));
            Check.Length(address, nameof(address), HospitalConsts.AddressMaxLength);
             
            var hospital = new Hospital(
             GuidGenerator.Create(),
            name,
            address
             );

            await SetDepartmentsAsync(hospital, departmentNames);

            return await hospitalRepository.InsertAsync(hospital);
        }

        public async virtual Task<Hospital> UpdateAsync(
            Guid id,
            string name,
            string address,
            [CanBeNull] string[] departmentNames,
            [CanBeNull] string? concurrencyStamp = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.Length(name, nameof(name), HospitalConsts.NameMaxLength);

            var hospital = (await hospitalRepository
              .GetQueryableAsync())
              .Where(h => h.Id == id)
              .FirstOrDefault();

            if (hospital == null)
            {
                throw new EntityNotFoundException(typeof(Hospital), id);
            }

            await hospitalRepository.EnsureCollectionLoadedAsync(hospital, h => h.HospitalDepartments);

            hospital.Name = name;

            Check.NotNullOrWhiteSpace(address, nameof(address));
            Check.Length(hospital.Address, nameof(address), HospitalConsts.AddressMaxLength);

            hospital.Address = address;

            await SetDepartmentsAsync(hospital, departmentNames);

            return await hospitalRepository.UpdateAsync(hospital);
        } 

        public async virtual Task DeleteAsyncHospitalWithDepartment(Guid id)
        {
            var hospital = (await hospitalRepository
             .GetQueryableAsync())
             .Where(h => h.Id == id)
             .FirstOrDefault();

            await DeleteDepartments(hospital);

            await hospitalRepository.DeleteAsync(hospital);
        }
         
        private async Task SetDepartmentsAsync(Hospital hospital, [CanBeNull] string[] departmentNames)
        {
            if (departmentNames == null || !departmentNames.Any())
            {
                hospital.RemoveAllDepartments();
                return;
            }

            var query = (await departmentRepository.GetQueryableAsync())
                    .Where(d => departmentNames.Contains(d.Name))
                    .Select(d => d.Id)
                    .Distinct();

            var departmentIds = await AsyncExecuter.ToListAsync(query);

            if (!departmentIds.Any())
                return;
              
            hospital.RemoveAllDepartmentsExceptGivenIds(departmentIds);

            foreach (var departmentId in departmentIds)
            {
                hospital.AddDepartment(departmentId);
            } 
        }

        private async Task DeleteDepartments(Hospital hospital)
        {
            await hospitalRepository.EnsureCollectionLoadedAsync(hospital, h => h.HospitalDepartments);
            hospital.HospitalDepartments.Clear();
        } 
    }
}
