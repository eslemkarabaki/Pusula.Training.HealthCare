using Pusula.Training.HealthCare.RadiologyExaminations;
using Pusula.Training.HealthCare.RadiologyRequests;

namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public class RadiologyRequestItemWithNavigationProperties
{
    public RadiologyExamination RadiologyExamination { get; set; } = null!;
    public RadiologyRequestItem RadiologyRequestItem { get; set; } = null!;
    public RadiologyRequest RadiologyRequest { get; set; } = null!;
}
