using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.RadiologyExaminationProcedures
{
    public class RadiologyExaminationProcedureExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Result { get; set; }
        public DateTime ResultDate { get; set; }
    }
}
