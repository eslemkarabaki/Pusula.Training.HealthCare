using Pusula.Training.HealthCare.RadiologyExaminationProcedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public class RadiologyExaminationDocumentWithNavigationPropertiesDto
    {
        public RadiologyExaminationDocumentDto RadiologyExaminationDocument { get; set; }
        public RadiologyExaminationProcedureDto RadiologyExaminationProcedure { get; set; }
    }
}
