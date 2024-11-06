using JetBrains.Annotations;
using Pusula.Training.HealthCare.HospitalDepartments;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing; 


namespace Pusula.Training.HealthCare.Hospitals
{
    public class Hospital : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Address { get; set; }

        public ICollection<HospitalDepartment> HospitalDepartments { get; set; }

        protected Hospital()
        {
            Name = string.Empty;
            Address = string.Empty;
            HospitalDepartments = new List<HospitalDepartment>();  
        }

        public Hospital(Guid id, string name, string address)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), HospitalConsts.NameMaxLength, 0);
            Name = name;
            Check.NotNull(address, nameof(address));
            Check.Length(address, nameof(address), HospitalConsts.AddressMaxLength, 0);
            Address = address;
            HospitalDepartments = new List<HospitalDepartment>();
        }

        public void AddDepartment(Guid departmentId)
        {
            Check.NotNull(departmentId, nameof(departmentId));

            if (IsInDepartment(departmentId))
                return;

            HospitalDepartments.Add(new HospitalDepartment(Id, departmentId, true));
        }

        public void RemoveDepartment(Guid departmentId)
        {
            Check.NotNull(departmentId, nameof(departmentId));

            if (!IsInDepartment(departmentId))
                return;

            HospitalDepartments.RemoveAll(hd => hd.DepartmentId == departmentId);
        }

        public void RemoveAllDepartments() => HospitalDepartments.RemoveAll(hd => hd.HospitalId == Id);

        public void RemoveAllDepartmentsExceptGivenIds(List<Guid> departmentIds)
        {
            Check.NotNull(departmentIds, nameof(departmentIds));

            HospitalDepartments.RemoveAll(hd => !departmentIds.Contains(hd.DepartmentId));
        }

        private bool IsInDepartment(Guid departmentId) => HospitalDepartments.Any(hd => hd.DepartmentId == departmentId);

    }
}
