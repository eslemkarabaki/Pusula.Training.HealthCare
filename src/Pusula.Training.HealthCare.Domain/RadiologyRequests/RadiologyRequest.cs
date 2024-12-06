using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.RadiologyRequests
{
    public class RadiologyRequest : FullAuditedAggregateRoot<Guid>
    {
        public virtual DateTime RequestDate { get; private set; }
        public virtual Guid ProtocolId { get; private set; }
        public virtual Guid DepartmentId { get; private set; }
        public virtual Guid DoctorId { get; private set; }
        
        protected RadiologyRequest() 
        { 
            RequestDate = DateTime.Now;
        }

        public RadiologyRequest(Guid id, DateTime requestDate, Guid protocolId, Guid departmentId, Guid doctorId)
        {
            SetRequestDate(requestDate);
            SetProtocolId(protocolId);
            SetDepartmentId(departmentId);
            SetDoctorId(doctorId);
        }

        public void SetRequestDate(DateTime requestDate) => RequestDate = Check.NotNull(requestDate, nameof(requestDate));
        public void SetProtocolId(Guid protocolId) => ProtocolId = Check.NotDefaultOrNull<Guid>(protocolId, nameof(protocolId));
        public void SetDepartmentId(Guid departmentId) => DepartmentId = Check.NotDefaultOrNull<Guid>(departmentId, nameof(departmentId));
        public void SetDoctorId(Guid doctorId) => DoctorId = Check.NotDefaultOrNull<Guid>(doctorId, nameof(doctorId));

    }
}
