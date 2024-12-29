using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Protocols;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Medical;

public partial class PatientFiles
{
    [Parameter]
    public int ProtocolNo { get; set; }

    [CascadingParameter]
    private ProtocolDto Protocol { get; set; } = null!;
}