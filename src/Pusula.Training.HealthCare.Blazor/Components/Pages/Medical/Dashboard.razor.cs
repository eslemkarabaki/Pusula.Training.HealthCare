using System;
using Microsoft.AspNetCore.Components;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.Medical;

public partial class Dashboard
{
    [Parameter]
    public int ProtocolNo { get; set; }
}