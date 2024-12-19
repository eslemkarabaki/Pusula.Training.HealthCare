using JetBrains.Annotations;
using Pusula.Training.HealthCare.Diagnoses;
using Pusula.Training.HealthCare.ExaminationsPhysical;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.Examinations
{
    public class Examination : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProtocolId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string SummaryDocument { get; set; }
        public DateTime StartDate { get; set; }


        public Examination(
            Guid id,
            Guid protocolId,
            Guid doctorId,
            Guid patientId,
            string summaryDocument,
            DateTime startDate)
            : base(id)
        {
            ProtocolId = protocolId;
            DoctorId = doctorId;
            PatientId = patientId;
            SummaryDocument = summaryDocument;
            StartDate = startDate;
        }
    }
}
