using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.ExaminationsPhysical
{
    public class GetExaminationPhysicalInput : PagedAndSortedResultRequestDto
    {
        public Guid? ExaminationId { get; set; }
        public Guid? UserId { get; set; }
        public string PhysicalNote { get; set; }
    }
}
