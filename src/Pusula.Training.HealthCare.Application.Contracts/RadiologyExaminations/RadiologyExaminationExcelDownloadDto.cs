using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class RadiologyExaminationExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public string? ExaminationCode { get; set; }

        public Guid GroupId { get; set; }

        public RadiologyExaminationExcelDownloadDto()
        {
        }
    }
}
