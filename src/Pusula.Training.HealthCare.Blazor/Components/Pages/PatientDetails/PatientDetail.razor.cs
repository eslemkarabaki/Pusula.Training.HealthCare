using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Pusula.Training.HealthCare.Appointments;
using Pusula.Training.HealthCare.Patients;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages.PatientDetails;

public partial class PatientDetail
{
    [Parameter] public int PatientNo { get; set; }
    [CascadingParameter] private PatientWithNavigationPropertiesDto Patient { get; set; } = null!;

    private IReadOnlyList<AppointmentWithNavigationPropertiesDto> Appointments { get; set; } = [];

    protected override async Task OnInitializedAsync() => await GetPatientWaitingAppointmentsAsync();

    private async Task GetPatientWaitingAppointmentsAsync() =>
        Appointments = await AppointmentsAppService.GetListWithNavigationPropertiesAsync(
            new GetAppointmentsInput()
            {
                PatientId = Patient.Patient.Id,
                StartTime = DateTime.Now
            }
        );
}