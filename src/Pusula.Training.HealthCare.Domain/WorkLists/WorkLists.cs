using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Tests;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.WorkLists
{
    public class WorkList : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid PatientId { get; set; } 
        public virtual Patient Patient { get; set; }

        public virtual Guid DoctorId { get; set; } 
        public virtual Doctor Doctor { get; set; } 

        public virtual Guid TestId { get; set; } 
        public virtual Test Test { get; set; } 

        public virtual DateTime ScheduledDate { get; set; } 
        public virtual bool IsCompleted { get; set; } 

        protected WorkList()
        {
        }

        public WorkList(
            Guid id,
            Guid patientId,
            Guid doctorId,
            Guid testId,
            DateTime scheduledDate,
            bool isCompleted)
        {
            Id = id;
            PatientId = patientId;
            DoctorId = doctorId;
            TestId = testId;
            ScheduledDate = scheduledDate;
            IsCompleted = isCompleted;
        }
    }
}
