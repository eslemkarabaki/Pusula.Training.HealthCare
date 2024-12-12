using System.Collections.Generic;
using Pusula.Training.HealthCare.ProtocolTypeActions;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public class ProtocolTypeWithNavigationPropertiesDto
{
    public ProtocolTypeDto ProtocolType { get; set; }
    public IEnumerable<ProtocolTypeActionDto> ProtocolTypeActions { get; set; }
}