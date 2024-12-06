using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadiologyRequests; 

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItemWithNavigationPropertiesDto
{
    public RadiologyRequestItemDto RadiologyRequestItem { get; set; } = null!;
    public RadiologyRequestDto RequestId { get; set; } = null!;
    public RadiologyExaminationDto ExaminationId { get; set; } = null!;
}
