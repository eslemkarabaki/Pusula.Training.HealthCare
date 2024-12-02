using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public class GetRadiologyExaminationGroupsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public GetRadiologyExaminationGroupsInput()
        {
        }
    }
}
