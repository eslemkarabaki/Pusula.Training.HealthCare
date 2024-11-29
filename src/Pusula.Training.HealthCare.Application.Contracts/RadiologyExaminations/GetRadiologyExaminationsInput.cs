using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.RadiologyExaminations
{
    public class GetRadiologyExaminationsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public string? Name { get; set; }
        public string? ExaminationCode { get; set; }
        public Guid? GroupId { get; set; }
    }
}
