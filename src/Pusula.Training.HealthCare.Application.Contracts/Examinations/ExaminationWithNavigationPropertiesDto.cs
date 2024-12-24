using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.ExaminationsPhysical;
using Pusula.Training.HealthCare.Protocols;
namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationWithNavigationPropertiesDto
    {
        public ExaminationDto Examination { get; set; }
        public ProtocolDto Protocol { get; set; }
        public ExaminationAnamnezDto ExaminationAnamnez { get; set; }
        public ExaminationDiagnosisDto ExaminationDiagnoses { get; set; }
        public ExaminationPhysicalDto ExaminationPhysical { get; set; }    
    }
}
