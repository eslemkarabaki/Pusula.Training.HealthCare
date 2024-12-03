using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public class RadiologyExaminationProcedure : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public string Result { get; set; }

        [NotNull]
        public DateTime ResultDate { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ProtocolId { get; set; }
        public Guid RadiologyExaminationId { get; set; }

        protected RadiologyExaminationProcedure()
        {
            Result = string.Empty;
            ResultDate = DateTime.MinValue;
        } 

        public RadiologyExaminationProcedure(
            Guid id,
            string result,
            DateTime resultDate,
            Guid doctorId,
            Guid protocolId,
            Guid RadiologyExaminationId)
        {
            Id = id;
            Check.NotNull(result, nameof(result));
            Check.Length(result, nameof(result), RadiologyExaminationProcedureConsts.MaxResultLength, RadiologyExaminationProcedureConsts.MinResultLength);
            Result = result;
            Check.NotNull(resultDate, nameof(resultDate));
            //Check.Range(resultDate, nameof(resultDate), 1, RadiologyExaminationProcedureConsts.MaxResultLength);
            ResultDate = resultDate;
            DoctorId = doctorId;
            ProtocolId = protocolId;
            RadiologyExaminationId = RadiologyExaminationId;
        }
    }
}
