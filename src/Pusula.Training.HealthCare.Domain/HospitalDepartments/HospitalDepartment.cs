using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
namespace Pusula.Training.HealthCare.HospitalDepartments
{
    public class HospitalDepartment : Entity 
    {
        [Key]
        public int Id { get; set; }
        [NotNull]
        public Guid HospitalId { get; set; }
        [NotNull]
        public Guid DepartmentId { get; set; } 

        public bool IsActive { get; set; } 

        protected HospitalDepartment()
        {
            HospitalId = default;
            DepartmentId = default;
            IsActive = true;
        }

        public HospitalDepartment(Guid hospitalId, Guid departmentId, bool isActive)
        {
            Check.NotNull(hospitalId, nameof(hospitalId));
            Check.NotNull(departmentId, nameof(departmentId));  
            HospitalId = hospitalId;
            DepartmentId = departmentId;
            IsActive = isActive;
        }

        public override object?[] GetKeys() => new object[] { HospitalId, DepartmentId };
    }
}
