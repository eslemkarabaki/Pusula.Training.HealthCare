using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.ExaminationsPhysical;
namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationWithNavigationProperties
    {
        public Examination Examination { get; set; }
        public ExaminationAnamnez ExaminationAnamnez { get; set; }
        public ExaminationDiagnosis ExaminationDiagnoses { get; set; }
        public ExaminationPhysical ExaminationPhysical { get; set; }
    }
}
