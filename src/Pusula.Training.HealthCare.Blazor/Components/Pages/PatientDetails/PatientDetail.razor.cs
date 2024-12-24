using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.PatientDetails;

public partial class PatientDetail
{
    [Parameter]
    public int PatientNo { get; set; }

    [CascadingParameter]
    private PatientWithNavigationPropertiesDto Patient { get; set; } = null!;
}