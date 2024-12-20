using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.ExaminationsPhysical;
namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationWithNavigationPropertiesDto
    {
        public ExaminationDto Examination { get; set; }
        public ExaminationAnamnezDto ExaminationAnamnez { get; set; }
        public ExaminationDiagnosisDto ExaminationDiagnoses { get; set; }
        public ExaminationPhysicalDto ExaminationPhysical { get; set; }
    }
}
