using System;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class TestProcessExcelDto
    {
        public string PatientName { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public string TestName { get; set; } = null!;
        public string Result { get; set; } = null!;
        public DateTime ResultDate { get; set; }
    }
}
