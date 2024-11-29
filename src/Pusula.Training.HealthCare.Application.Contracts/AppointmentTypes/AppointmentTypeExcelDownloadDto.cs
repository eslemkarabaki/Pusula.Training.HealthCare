using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeExcelDownloadDto
    {
        public string DownloadToken { get; set; } = null!;
        public string? FilterText { get; set; }
        public string? Name { get; set; }

        public AppointmentTypeExcelDownloadDto() { }
    }
}
