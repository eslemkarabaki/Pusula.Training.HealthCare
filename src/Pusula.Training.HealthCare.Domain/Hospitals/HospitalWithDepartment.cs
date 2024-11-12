using System;
using Volo.Abp.Auditing;

namespace Pusula.Training.HealthCare.Hospitals
{
    public class HospitalWithDepartment : IHasCreationTime
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual string Name { get; set; }
         
        public virtual string Address { get; set; }

        public virtual string[] DepartmentNames { get; set; }

    }
}
