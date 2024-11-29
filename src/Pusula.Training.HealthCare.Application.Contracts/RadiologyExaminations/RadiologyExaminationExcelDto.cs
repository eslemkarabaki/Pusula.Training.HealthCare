using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class RadiologyExaminationExcelDto
    {
        public string Name { get; set; } = null!;

        public string ExaminationCode { get; set; } = null!;

        public Guid GroupId { get; set; }
    }
}
