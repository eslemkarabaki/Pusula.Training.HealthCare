using System; 
using Volo.Abp.Auditing;

namespace Pusula.Training.HealthCare.Departments
{
    public class DepartmentWithHospital : IHasCreationTime
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Duration { get; set; }
        public virtual string[] HospitalNames { get; set; }

    }
}
