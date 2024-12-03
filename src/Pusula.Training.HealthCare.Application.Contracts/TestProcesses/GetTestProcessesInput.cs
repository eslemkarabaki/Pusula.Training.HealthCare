using Volo.Abp.Application.Dtos;
using System;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class GetTestProcessesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? TestId { get; set; }
        public string? Result { get; set; }
        public DateTime? ResultDateStart { get; set; }
        public DateTime? ResultDateEnd { get; set; }
    }
}
