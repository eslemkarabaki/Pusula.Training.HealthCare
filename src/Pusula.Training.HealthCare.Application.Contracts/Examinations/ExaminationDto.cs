using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Examinations
{
    public class ExaminationDto : EntityDto<Guid>
    {
        public Guid ProtocolId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public string SummaryDocument { get; set; }
        public DateTime StartDate { get; set; }

        public int ProtocolNo { get; set; }

    }
}
