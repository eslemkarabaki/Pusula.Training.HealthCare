using System.Collections.Generic;
using Pusula.Training.HealthCare.ProtocolTypeActions;

namespace Pusula.Training.HealthCare.ProtocolTypes;

public class ProtocolTypeWithNavigationProperties
{
    public ProtocolType ProtocolType { get; set; }
    public IEnumerable<ProtocolTypeAction> ProtocolTypeActions { get; set; }
}