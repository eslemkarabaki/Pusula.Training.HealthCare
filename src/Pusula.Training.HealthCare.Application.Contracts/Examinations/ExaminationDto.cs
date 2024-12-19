using System;

namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationDto
    {
        public Guid Id { get; set; }
        public Guid ProtocolId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string SummaryDocument { get; set; }
        public DateTime StartDate { get; set; }
    }
}
