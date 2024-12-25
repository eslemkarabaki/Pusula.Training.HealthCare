using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.ExaminationsPhysical;
using System;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationUpdateDto
    {
        public  ExaminationDiagnosisUpdateDto DiagnosisUpdateDto { get; set; }

        public ExaminationAnamnezUpdateDto AnamnezUpdateDto { get; set; }
        
        public ExaminationPhysicalUpdateDto PhysicalUpdateDto { get; set; }
        
        public string? SummaryDocument { get; set; } 
        public Guid ProtocolId { get; set; }
        public Guid Id { get; set; }
    }
}
