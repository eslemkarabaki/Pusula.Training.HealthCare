using System;
using JetBrains.Annotations;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Tests;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class TestProcess : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid PatientId { get; set; } 
        public virtual Patient Patient { get; set; } 

        public virtual Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } 

        public virtual Guid TestId { get; set; } 
        public virtual Test Test { get; set; } 

        public virtual string Result { get; set; } 
        public virtual DateTime ResultDate { get; set; } 

        public virtual Guid DepartmentId { get; set; } 

        protected TestProcess()
        {
            Result = string.Empty;
        }

        public TestProcess(Guid id, Guid patientId, Guid doctorId, Guid testId, string result, DateTime resultDate, Guid departmentId)
        {
            Id = id;
            PatientId = patientId;
            DoctorId = doctorId;
            TestId = testId;

            Check.NotNullOrWhiteSpace(result, nameof(result));

            Result = result;
            ResultDate = resultDate;
            DepartmentId = departmentId;
        }
    }
}
